using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VPServiceLibrary
{
    public class VPService : IVPService
    {
       

        public DenunciaWS[] GetNotificaciones()
        {
            var db = new VPContext();
            List<DenunciaWS> lista = new List<DenunciaWS>();

            var query = from a in db.Denuncias
                        where a.Estado == "Espera"
                        select a;

            if (query.Count() <= 0)
            {
                return null;
            }

            foreach (var denuncia in query)
            {
                // No todos los atributos de agente son relevante en esta consulta
                DenunciaWS newDenuncia = new DenunciaWS
                {
                    Tipo = denuncia.Tipo,
                    Id = denuncia.ID_Denuncia,
                    Fecha_creacion = denuncia.Fecha_Creacion
                };
                lista.Add(newDenuncia);
            }

            return lista.ToArray();
        }

        public bool DeleteDenuncia(int idDenuncia)
        {
            var db = new VPContext();

            try
            {

            var query = from d in db.Denuncias
                         where d.ID_Denuncia == idDenuncia
                         select d;

            if (query.Count() == 0)
                return false;

            
            db.Denuncias.Remove(query.First());

            
                db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return true;

        }

        public DenunciaWS GetDenuncia(int idDenuncia)
        {
            var db = new VPContext();

            var query = from a in db.Denuncias
                        where a.ID_Denuncia == idDenuncia
                        select a;

            if (query.Count() <= 0)
                return null;

            Denuncia denuncia = query.First();

            DenunciaWS newDenuncia = new DenunciaWS
            {
                Tipo = denuncia.Tipo,
                Descripcion_hecho = denuncia.Decripcion_Hecho,
                Fecha_creacion = denuncia.Fecha_Creacion,
                DNI_Denunciante = denuncia.Denunciante.DNI,
                Direccion_hecho = denuncia.Direccion_Hecho,
                Fecha_hecho = denuncia.Fecha_Hecho,
                Hora_hecho = denuncia.Hora_Hecho,
                Naturaleza_lugar_hecho = denuncia.Naturaleza_Lugar_Hecho,
                Id = denuncia.ID_Denuncia,
                NumPlaca = denuncia.Agente.NumPlaca
            };

            if (denuncia.Expediente != null)
            {
                newDenuncia.DNI_expediente = denuncia.Expediente.Ciudadano.DNI;
            }

            if (denuncia.Tipo.Equals("Robo de vehiculo"))
            {
                RoboCoche robo = (from a in db.Denuncias.OfType<RoboCoche>()
                        where a.ID_Denuncia == idDenuncia
                        select a).First();

                RoboCocheWS robocoche = new RoboCocheWS
            {
                    Color = robo.Vehiculo.Color,
                    Marca = robo.Vehiculo.Marca,
                    Matricula = robo.Vehiculo.Matricula,
                    Modelo = robo.Vehiculo.Modelo,
                    NumBastidor = robo.Vehiculo.NumBastidor
            };
            
                newDenuncia.RoboCoche = robocoche;
            }

            if (denuncia.Tipo.Equals("Secuestro"))
            {
                Secuestro secu = (from a in db.Denuncias.OfType<Secuestro>()
                                  where a.ID_Denuncia == idDenuncia
                                  select a).First();
            SecuestroWS s = new SecuestroWS
            {
                    DNI_Ciudadano = secu.Ciudadano.DNI
            };

                newDenuncia.Secuestro = s;
            }

            if (denuncia.Tipo.Equals("Desaparicion"))
            {
                Desaparicion des = (from a in db.Denuncias.OfType<Desaparicion>()
                                    where a.ID_Denuncia == idDenuncia
                                    select a).First();

            FotoWS f = new FotoWS
            {
                Image = des.Foto.Image,
                Size = des.Foto.Size,
                Type = des.Foto.Type
            };

            DesaparicionWS d = new DesaparicionWS
            {
                Descripcion_fisica = des.Descripción_Fisica,
                DNI_Desaparecido = des.Ciudadano.DNI,
                Foto = f
            };

                newDenuncia.Desaparicion = d;
            }

            return newDenuncia;
        }

        public int AddDenuncia(DenunciaWS newDenuncia)
        {
            var db = new VPContext();

            Denuncia denuncia;

            try
            {

                switch (newDenuncia.Tipo)
                {
                    case "Desaparicion":
                        denuncia = new Desaparicion();

                        denuncia.Agente = (from c in db.Agentes
                                               where c.NumPlaca == newDenuncia.NumPlaca
                                               select c).First();
                        denuncia.Denunciante = (from c in db.Ciudadanos
                                                where c.DNI == newDenuncia.DNI_Denunciante
                                                select c).First();
                        (denuncia as Desaparicion).Ciudadano = (from c in db.Ciudadanos
                                                  where c.DNI == newDenuncia.Desaparicion.DNI_Desaparecido
                                                  select c).First();
                        denuncia.Tipo = newDenuncia.Tipo;
                        denuncia.Estado = "Activa";
                        denuncia.Fecha_Creacion = DateTime.Now;
                        denuncia.Direccion_Hecho = newDenuncia.Direccion_hecho;
                        denuncia.Naturaleza_Lugar_Hecho = newDenuncia.Naturaleza_lugar_hecho;
                        denuncia.Fecha_Hecho = newDenuncia.Fecha_hecho;
                        denuncia.Hora_Hecho = newDenuncia.Hora_hecho;
                        denuncia.Decripcion_Hecho = newDenuncia.Descripcion_hecho;
                        (denuncia as Desaparicion).Descripción_Fisica = newDenuncia.Desaparicion.Descripcion_fisica;
                    Foto f = new Foto
                    {
                        Image = newDenuncia.Desaparicion.Foto.Image,
                        Size = newDenuncia.Desaparicion.Foto.Size,
                        Type = newDenuncia.Desaparicion.Foto.Type
                    };
                        db.Fotos.Add(f);
                        (denuncia as Desaparicion).Foto = f;
                        db.Denuncias.Add(denuncia);
                        break;

                    case "Secuestro":
                        denuncia = new Secuestro();

                        denuncia.Agente = (from c in db.Agentes
                                            where c.NumPlaca == newDenuncia.NumPlaca
                                            select c).First();
                        denuncia.Denunciante = (from c in db.Ciudadanos
                                             where c.DNI == newDenuncia.DNI_Denunciante
                                             select c).First();
                        (denuncia as Secuestro).Ciudadano = (from c in db.Ciudadanos
                                               where c.DNI == newDenuncia.Secuestro.DNI_Ciudadano
                                               select c).First();
                        denuncia.Tipo = newDenuncia.Tipo;
                        denuncia.Estado = "Activa";
                        denuncia.Fecha_Creacion = DateTime.Now;
                        denuncia.Direccion_Hecho = newDenuncia.Direccion_hecho;
                        denuncia.Naturaleza_Lugar_Hecho = newDenuncia.Naturaleza_lugar_hecho;
                        denuncia.Fecha_Hecho = newDenuncia.Fecha_hecho;
                        denuncia.Hora_Hecho = newDenuncia.Hora_hecho;
                        denuncia.Decripcion_Hecho = newDenuncia.Descripcion_hecho;
                        db.Denuncias.Add(denuncia);
                        break;

                    case "Robo de vehiculo":
                        denuncia = new RoboCoche();

                        denuncia.Agente = (from c in db.Agentes
                                            where c.NumPlaca == newDenuncia.NumPlaca
                                            select c).First();
                        denuncia.Denunciante = (from c in db.Ciudadanos
                                             where c.DNI == newDenuncia.DNI_Denunciante
                                             select c).First();
                        denuncia.Tipo = newDenuncia.Tipo;
                        denuncia.Estado = "Activa";
                        denuncia.Fecha_Creacion = DateTime.Now;
                        denuncia.Direccion_Hecho = newDenuncia.Direccion_hecho;
                        denuncia.Naturaleza_Lugar_Hecho = newDenuncia.Naturaleza_lugar_hecho;
                        denuncia.Fecha_Hecho = newDenuncia.Fecha_hecho;
                        denuncia.Hora_Hecho = newDenuncia.Hora_hecho;
                        denuncia.Decripcion_Hecho = newDenuncia.Descripcion_hecho;
                    Vehiculo v = new Vehiculo
                    {
                        Color = newDenuncia.RoboCoche.Color,
                        Marca = newDenuncia.RoboCoche.Marca,
                        Matricula = newDenuncia.RoboCoche.Matricula,
                        Modelo = newDenuncia.RoboCoche.Modelo,
                        NumBastidor = newDenuncia.RoboCoche.NumBastidor,
                        CiudadanoID_Ciudadano = denuncia.Denunciante.ID_Ciudadano
                    };
                        db.Vehiculos.Add(v);
                        (denuncia as RoboCoche).Vehiculo = v;
                        db.Denuncias.Add(denuncia);
                        break;

                    default:
                        denuncia = new Denuncia();

                        denuncia.Agente = (from c in db.Agentes
                                           where c.NumPlaca == newDenuncia.NumPlaca
                                           select c).First();
                    denuncia.Denunciante = (from c in db.Ciudadanos
                                            where c.DNI == newDenuncia.DNI_Denunciante
                                            select c).First();
                        denuncia.Tipo = newDenuncia.Tipo;
                        denuncia.Estado = "Activa";
                        denuncia.Fecha_Creacion = DateTime.Now;
                        denuncia.Direccion_Hecho = newDenuncia.Direccion_hecho;
                        denuncia.Naturaleza_Lugar_Hecho = newDenuncia.Naturaleza_lugar_hecho;
                        denuncia.Fecha_Hecho = newDenuncia.Fecha_hecho;
                        denuncia.Hora_Hecho = newDenuncia.Hora_hecho;
                        denuncia.Decripcion_Hecho = newDenuncia.Descripcion_hecho;
                        db.Denuncias.Add(denuncia);
                        break;
                }
           
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                return -1;
            }
            catch(System.InvalidOperationException)
            {
                return -1;
            }

            return denuncia.ID_Denuncia;

        }

        public bool AddAgente(AgenteWS newAgente)
        {
            var db = new VPContext();

            Agente agente = db.Agentes.Create();
            try
            {

            agente.Ciudadano = (from c in db.Ciudadanos
                                where c.DNI == newAgente.DNI
                                select c).First();

            agente.Fecha_Creacion = DateTime.Now;
            agente.NumPlaca = newAgente.NumPlaca;
            agente.Password = generarPassword(6);
            agente.Email = newAgente.Email;
            agente.Rango = newAgente.Rango;
            agente.Telefono = newAgente.Telefono;
            agente.Estado_Civil = newAgente.Estado_Civil;
            agente.Observaciones = newAgente.Observaciones;

            db.Agentes.Add(agente);

            
                db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return true;
        }

        private string generarPassword(int PasswordLength)
        { 
            string allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789"; 
            Byte[] randomBytes = new Byte[PasswordLength]; 
            char[] chars = new char[PasswordLength]; 
            int allowedCharCount = allowedChars.Length; 

            for (int i = 0; i < PasswordLength; i++)
            { 
                Random randomObj = new Random(); 
                randomObj.NextBytes(randomBytes); 
                chars[i] = allowedChars[(int)randomBytes[i] % allowedCharCount]; 
            } 

            return new string(chars); 
        }

        public bool AddCiudadano(CiudadanoWS newCiudadano)
        {
            var db = new VPContext();

            Ciudadano ciudadano = db.Ciudadanos.Create();

            int count = (from c in db.Ciudadanos
                        where c.DNI == newCiudadano.DNI
                        select c).Count();

            if (count > 0)
                return false;

            ciudadano.DNI = newCiudadano.DNI;
            ciudadano.Nombre = newCiudadano.Nombre;
            ciudadano.Apellido1 = newCiudadano.Apellido1;
            ciudadano.Apellido2 = newCiudadano.Apellido2;
            ciudadano.Fecha_Nacimiento = newCiudadano.Fecha_Nacimiento;
            ciudadano.Sexo = newCiudadano.Sexo;
            ciudadano.Calle = newCiudadano.Calle;
            ciudadano.Numero = newCiudadano.Numero;
            ciudadano.Letra_Piso = newCiudadano.Letra_Piso;
            ciudadano.Localidad = newCiudadano.Localidad;
            ciudadano.Provincia = newCiudadano.Provincia;
            ciudadano.CP = newCiudadano.CP;
            ciudadano.Nacionalidad = newCiudadano.Nacionalidad;
            ciudadano.Huella_Dactilar = newCiudadano.Huella_Dactilar;

            db.Ciudadanos.Add(ciudadano);

            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                return false;
            }

            return true;
        }

        public CiudadanoWS GetCiudadano(string dni)
        {
            var db = new VPContext();
            CiudadanoWS ciudadano = new CiudadanoWS();

            var query = from c in db.Ciudadanos
                        where c.DNI == dni
                        select c;

            if (query.Count() <= 0)
                return null;

            Ciudadano newCiudadano = query.First();

            ciudadano.DNI = newCiudadano.DNI;
            ciudadano.Nombre = newCiudadano.Nombre;
            ciudadano.Apellido1 = newCiudadano.Apellido1;
            ciudadano.Apellido2 = newCiudadano.Apellido2;
            ciudadano.Fecha_Nacimiento = newCiudadano.Fecha_Nacimiento;
            ciudadano.Sexo = newCiudadano.Sexo;
            ciudadano.Calle = newCiudadano.Calle;
            ciudadano.Numero = newCiudadano.Numero;
            ciudadano.Letra_Piso = newCiudadano.Letra_Piso;
            ciudadano.Localidad = newCiudadano.Localidad;
            ciudadano.Provincia = newCiudadano.Provincia;
            ciudadano.CP = newCiudadano.CP;
            ciudadano.Nacionalidad = newCiudadano.Nacionalidad;

            return ciudadano;
        }

        public AgenteWS GetAgente(string numPlaca)
        {
            var db = new VPContext();

            var query = from a in db.Agentes
                        where a.NumPlaca == numPlaca
                        select a;

            if (query.Count() <= 0)
                return null;

            Agente agente = query.First();

            AgenteWS newAgente = new AgenteWS
            {
                DNI = agente.Ciudadano.DNI,
                Nombre = agente.Ciudadano.Nombre,
                Apellido1 = agente.Ciudadano.Apellido1,
                Apellido2 = agente.Ciudadano.Apellido2,
                NumPlaca = agente.NumPlaca,
                Rango = agente.Rango,
                Email = agente.Email,
                Estado_Civil = agente.Estado_Civil,
                Telefono = agente.Telefono,
                Observaciones = agente.Observaciones
            };

            return newAgente;
        }

        public bool ComprobarPassword(string numPlaca, string pass)
        {
            var db = new VPContext();

            var query = from p in db.Agentes
                        where p.NumPlaca == numPlaca
                        select p.Password;

            if (query.Count() <= 0)
                return false;
            
            return query.First().Equals(pass);
        }

        public AgenteWS[] GetAgentes()
        {
            var db = new VPContext();
            List<AgenteWS> lista = new List<AgenteWS>();

            // La lista estará ordenada en orden alfabético
            var query = from a in db.Agentes
                        orderby a.Ciudadano.Nombre
                        select a;

            if (query.Count() <= 0)
            {
                return null;
            }
                      
            foreach (var agente in query)
            {
                // No todos los atributos de agente son relevante en esta consulta
                AgenteWS newAgente = new AgenteWS
                {
                    DNI = agente.Ciudadano.DNI,
                    Nombre = agente.Ciudadano.Nombre,
                    Apellido1 = agente.Ciudadano.Apellido1,
                    Apellido2 = agente.Ciudadano.Apellido2,
                    NumPlaca = agente.NumPlaca,
                    Rango = agente.Rango
                };
                lista.Add(newAgente);
            }

            return lista.ToArray();
        }

        public ActividadWS[] GetPlanning(string numPlaca, DateTime fecha, int dias)
        {
            var db = new VPContext();
            DateTime max_fecha = fecha.AddDays(dias);
            List<ActividadWS> planning = new List<ActividadWS>();

            // La lista está ordenada por fecha
            var query = from a in db.Actividades
                        orderby a.Fecha, a.Hora_Inicio
                        where a.Agente.NumPlaca == numPlaca && (fecha <= a.Fecha && a.Fecha < max_fecha)
                        select a;

            if (query.Count() <= 0)
                return null;

            foreach (var actividad in query)
            {
                ActividadWS newActividad = new ActividadWS
                {
                    Fecha = actividad.Fecha,
                    Hora_Inicio = actividad.Hora_Inicio,
                    Hora_Fin = actividad.Hora_Fin,
                    Tipo = actividad.Tipo,
                    Descripcion = actividad.Descripcion
                };
                planning.Add(newActividad);
            }
            return planning.ToArray();
        }

        public bool ComprobarCiuAgente(string numPlaca, string DNI)
        {
            var db = new VPContext();

            int num = (from a in db.Agentes
                       where a.Ciudadano.DNI == DNI
                       select a).Count();

            if (num > 0)
                return false;
            else
                return true;
        }

        public bool ComprobarConexion()
        {
            try
            {
                var db = new VPContext();

                int num = (from a in db.Agentes
                           select a).Count();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ExpedienteWS GetExpediente(string DNI)
        {
            FotoWS foto=null;
            var db = new VPContext();

            var query = from a in db.Expedientes
                        where a.Ciudadano.DNI == DNI
                        select a;

            if (query.Count() <= 0)
                return null;

            Expediente expediente = query.First();

            if (expediente.Foto != null)
            {
                foto = new FotoWS
                {
                    Image = expediente.Foto.Image,
                    Size = expediente.Foto.Size,
                    Type = expediente.Foto.Type
                };
            }

            ExpedienteWS newExpediente = new ExpedienteWS
            {

                DNI = expediente.Ciudadano.DNI,
                Altura = expediente.Altura.Value,
                Peso = expediente.Peso.Value,
                Edad = expediente.Edad,
                Estado_Civil = expediente.Estado_Civil,
                Telefono = expediente.Telefono,
                Foto=foto
              
            };
           
            return newExpediente;
        
        }

        public bool CrearExpediente(ExpedienteWS newExpediente)
        {
            
            var db = new VPContext();

            var query = from a in db.Expedientes
                        where a.Ciudadano.DNI == newExpediente.DNI
                        select a;

            if (query.Count() > 0) //si ya existe un expediente para ese ciudadano.
                return false;

            Expediente expediente = db.Expedientes.Create();
            try
            {

                expediente.Ciudadano = (from c in db.Ciudadanos
                                    where c.DNI == newExpediente.DNI
                                    select c).First();

                DateTime fechaNacimiento = expediente.Ciudadano.Fecha_Nacimiento;
                int edad = (DateTime.Now.Subtract(fechaNacimiento).Days / 365);

                //datos introducidos automaticamente
                expediente.Fecha_Creacion = DateTime.Now;
                expediente.Fecha_UltimaModif = DateTime.Now;
                expediente.Edad = edad;
                
                //datos que nos pasan en el nuevo expediente
                if (newExpediente.Foto != null)
                {
                    Foto f = new Foto();
                    f.Image = newExpediente.Foto.Image;
                    f.Size = newExpediente.Foto.Size;
                    f.Type = newExpediente.Foto.Type;
                    db.Fotos.Add(f);
                    expediente.Foto = f;
                
                }
               
                expediente.Altura = newExpediente.Altura;
                expediente.Peso = newExpediente.Peso;
                expediente.Estado_Civil = newExpediente.Estado_Civil;
                expediente.Telefono = newExpediente.Telefono;

                db.Expedientes.Add(expediente);
                db.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                return false;
            }

            return true;
       
        }

        public bool AsociarExpediente(int id_denuncia, string DNI)
        {

            var db = new VPContext();
            //Comprobamos que exista

            var query = from d in db.Denuncias
                        where d.ID_Denuncia == id_denuncia
                        select d;

            if (query.Count() == 0) return false; //no existe esa denuncia

            Denuncia denuncia = query.First();
            if (denuncia.Expediente != null) { return false; } //ya esta asociada a un expediente 
                try
                {

                    denuncia.Expediente = (from e in db.Expedientes
                                           where e.Ciudadano.DNI == DNI
                                           select e).First();

                    denuncia.Expediente.Fecha_UltimaModif = DateTime.Now;

                    db.SaveChanges();
                }
                catch (InvalidOperationException)
                {
                    return false;
                }


                return true;
            
        }

        public DenunciaWS[] GetDenuncias(string DNI)
        {
            var db = new VPContext();
            List<DenunciaWS> lista = new List<DenunciaWS>();

            var query = from a in db.Denuncias
                        where a.Estado == "Activa"
                        orderby a.ID_Denuncia
                        select a;

            if (DNI != null)
            {

                query = from a in db.Denuncias
                        where a.Expediente.Ciudadano.DNI == DNI
                        orderby a.ID_Denuncia
                        select a;
            }

            if (query.Count() <= 0)
            {
                return null;
            }

            foreach (var denuncia in query)
            {
                
                DenunciaWS newDenuncia = new DenunciaWS
                {
                    Tipo = denuncia.Tipo,
                    Descripcion_hecho = denuncia.Decripcion_Hecho,
                    Direccion_hecho = denuncia.Direccion_Hecho,
                    Fecha_hecho = denuncia.Fecha_Hecho,
                    Fecha_creacion = denuncia.Fecha_Creacion,
                    Hora_hecho = denuncia.Hora_Hecho,
                    Naturaleza_lugar_hecho = denuncia.Naturaleza_Lugar_Hecho,
                    Id = denuncia.ID_Denuncia,
                    NumPlaca = denuncia.Agente.NumPlaca,
                    DNI_expediente = DNI 

                };
                lista.Add(newDenuncia);
            }

            return lista.ToArray();
        }
    }
}
