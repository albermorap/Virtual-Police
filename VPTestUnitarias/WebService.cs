using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Drawing;
//lIBRERIAS NECESARIAS  para poder ejecutar los script SQL
using System.Configuration;//necesario para poder leer el ficher App.
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;//archivos de programa/sqlserver/v11/sdk/assemblies
using Microsoft.SqlServer.Management.Common;//archivos de programa/sqlserver/v11/sdk/assemblies

namespace VPTestUnitarias
{
    [TestClass]
    public class WebService
    {        

        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {/*
            //leer la conexión a la BD

            //mirar en el ficher App.config cuál es nombre de vuestro connectionstring
            string connection = ConfigurationManager.ConnectionStrings["VPContext"].ConnectionString;
            string current_path = System.IO.Directory.GetCurrentDirectory();

            //sustituir BloggingService por el nombre de la carpeta de la solución
            current_path = current_path.Substring(0, current_path.IndexOf("Proyecto\\") - 1);


            try
            {
                SqlConnection sqlConnection = new SqlConnection(connection);
                Server server = new Server(new ServerConnection(sqlConnection));

                //sustituir BloggingModel.edmx.sql por el nombre del script de creación de la base de datos
                FileInfo sqlFile = new FileInfo(current_path + "\\Proyecto\\VPTestUnitarias\\VPModel.edmx.sql");
                string script = sqlFile.OpenText().ReadToEnd();
                server.ConnectionContext.ExecuteNonQuery(script);


                //copiar y pegar las siguientes tres líneas por cada script de base de datos que queramos ejecutar
                sqlFile = new FileInfo(current_path + "\\Proyecto\\VPTestUnitarias\\script_inicial.sql");
                script = sqlFile.OpenText().ReadToEnd();
                server.ConnectionContext.ExecuteNonQuery(script);
            }
            catch (Exception e)
            {
                string rdo = e.Message;
            }*/
            }

        [TestCleanup()]
        public void TestCleanup()
        {
        }

        [TestInitialize()]
        public void TestInitialize()
        {
        }


        
        [TestMethod]
        [TestCategory("GetPlanning()")]
        [TestProperty("CajaBlanca","CaminoBasico")]
        public void GetPlanning_a()     
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String numPlaca = "1234K";
            int dias = 0;
            DateTime fecha = new DateTime(2014,10,24);
            
            VPService.ActividadWS[] planning;
            planning = servicio.GetPlanning(numPlaca,fecha,dias);
            Assert.IsNull(planning);
        }

        [TestMethod]
        [TestCategory("GetPlanning()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetPlanning_b()     
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String numPlaca = "1234K";
            int dias = 30;
            DateTime fecha = new DateTime(2014,10,23);

            VPService.ActividadWS[] planning;
            planning = servicio.GetPlanning(numPlaca,fecha,dias);
            Assert.IsNotNull(planning);
        }



        [TestMethod]
        [TestCategory("ComprobarCiuAgente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarCiuAgente_a()
        {   //el agente tiene asignado a ese ciudadano
            VPService.IVPService service = new VPService.VPServiceClient();
            String numPlaca = "1234K";
            String DNI = "06284403K";
            Boolean resultado_esperado = false;
            Boolean resultado_real = service.ComprobarCiuAgente(numPlaca,DNI);
            Assert.AreEqual(resultado_esperado, resultado_real);
        }

        [TestMethod]
        [TestCategory("ComprobarCiuAgente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarCiuAgente_b() 
        {   //el agente NO tiene asignado a ese ciudadano
            VPService.IVPService service = new VPService.VPServiceClient();
            String numPlaca = "1234K";
            String DNI = "99999999K";
            Boolean resultado_esperado = true;
            Boolean resultado_real = service.ComprobarCiuAgente(numPlaca,DNI);
            Assert.AreEqual(resultado_esperado, resultado_real);
        }



        [TestMethod]
        [TestCategory("GetAgente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetAgente_a()  
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String numPlaca = "";
            VPService.AgenteWS ag = new VPService.AgenteWS();
            ag = servicio.GetAgente(numPlaca);
            Assert.IsNull(ag);
        }

        [TestMethod]
        [TestCategory("GetAgente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetAgente_b()  
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String numPlaca = "1234K";
            VPService.AgenteWS ag = new VPService.AgenteWS();
            ag = servicio.GetAgente(numPlaca);
            Assert.IsNotNull(ag);
            Assert.AreEqual(ag.NumPlaca, numPlaca);
        }


        
        [TestMethod]
        [TestCategory("ComprobarPassword()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarPassword_a()       
        {
            VPService.IVPService service = new VPService.VPServiceClient();

            String numPlaca = "r_u4`p9";
            String pass = "";
            Boolean resultado_real = false;
            
            Assert.AreEqual(service.ComprobarPassword(numPlaca, pass), resultado_real);
        }

        [TestMethod]
        [TestCategory("ComprobarPassword()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarPassword_b()       
        {
            VPService.IVPService service = new VPService.VPServiceClient();

            String numPlaca = "1234L";
            String pass = "1234L";
            Boolean resultado_real = true;

            Assert.AreEqual(service.ComprobarPassword(numPlaca, pass), resultado_real);
        }

        [TestMethod]
        [TestCategory("ComprobarPassword()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarPassword_c()       
        {
            VPService.IVPService service = new VPService.VPServiceClient();

            String numPlaca = "1234K";
            String pass = "*^R_12";
            Boolean resultado_real = false;
  
            Assert.AreEqual(service.ComprobarPassword(numPlaca, pass), resultado_real);
        }



        [TestMethod]
        [TestCategory("GetCiudadano()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetCiudadano_a()    
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String dni = "";
            VPService.CiudadanoWS ciu = new VPService.CiudadanoWS();
            ciu = servicio.GetCiudadano(dni);
            Assert.IsNull(ciu);
        }

        [TestMethod]
        [TestCategory("GetCiudadano()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetCiudadano_b()    
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String dni = "88888888A";
            VPService.CiudadanoWS ciu = new VPService.CiudadanoWS();
            ciu = servicio.GetCiudadano(dni);
            Assert.IsNotNull(ciu);
            Assert.AreEqual(ciu.DNI, dni);
        }



        [TestMethod]
        [TestCategory("AddCiudadano()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddCiudadano_a()    
        {   //Ciudadano ya existe
            VPService.IVPService service = new VPService.VPServiceClient();
            VPService.CiudadanoWS ciudadano = new VPService.CiudadanoWS();
            Boolean resultado_esperado, resultado_real;

            ciudadano.DNI = "47448889G"; 
            ciudadano.Nombre="Juan";
            ciudadano.Apellido1="Hernandez";
            ciudadano.Apellido2="Mora";
            ciudadano.Sexo="H";
            ciudadano.Calle="Juana";
            ciudadano.Numero="8";
            ciudadano.Letra_Piso="A";
            ciudadano.Localidad="Albacete";
            ciudadano.Provincia="Albacete";
            ciudadano.CP="2002";
            ciudadano.Nacionalidad="Española";
            ciudadano.Fecha_Nacimiento = new DateTime(2010, 07, 25);

            resultado_esperado = false;
            resultado_real = service.AddCiudadano(ciudadano);

            Assert.AreEqual(resultado_esperado, resultado_real);
        }

        [TestMethod]
        [TestCategory("AddCiudadano()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddCiudadano_b()    
        {   //Campo obligatorio nulo
            VPService.IVPService service = new VPService.VPServiceClient();
            VPService.CiudadanoWS ciudadano = new VPService.CiudadanoWS();
            Boolean resultado_esperado, resultado_real;

            ciudadano.DNI = "47448866G";
            ciudadano.Nombre = null;
            ciudadano.Apellido1 = "Hernandez";
            ciudadano.Apellido2 = "Mora";
            ciudadano.Sexo = "H";
            ciudadano.Calle = "Juana";
            ciudadano.Fecha_Nacimiento = new DateTime(2010, 07, 25);
            ciudadano.Numero = "8";
            ciudadano.Letra_Piso = "A";
            ciudadano.Localidad = "Albacete";
            ciudadano.Provincia = "Albacete";
            ciudadano.CP = "2002";
            ciudadano.Nacionalidad = "Española";

            resultado_esperado = false;
            resultado_real = service.AddCiudadano(ciudadano);

            Assert.AreEqual(resultado_esperado, resultado_real);
        }

        [TestMethod]
        [TestCategory("AddCiudadano()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddCiudadano_c()    
        {   //Añadir uno correctamente
            VPService.IVPService service = new VPService.VPServiceClient();
            VPService.CiudadanoWS ciudadano = new VPService.CiudadanoWS();
            Boolean resultado_esperado, resultado_real;

            ciudadano.DNI = "47446666G";
            ciudadano.Nombre = "Andres";
            ciudadano.Apellido1 = "Hernandez";
            ciudadano.Apellido2 = "Mora";
            ciudadano.Sexo = "H";
            ciudadano.Calle = "Juana";
            ciudadano.Fecha_Nacimiento = new DateTime(2010, 09, 28);
            ciudadano.Numero = "8";
            ciudadano.Letra_Piso = "A";
            ciudadano.Localidad = "Albacete";
            ciudadano.Provincia = "Albacete";
            ciudadano.CP = "2002";
            ciudadano.Nacionalidad = "Española";

            resultado_esperado = true;
            resultado_real = service.AddCiudadano(ciudadano);

            Assert.AreEqual(resultado_esperado, resultado_real);
        }



        [TestMethod]
        [TestCategory("GetAgentes()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetAgentes_a()  
        {   //BD sin agentes
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.AgenteWS[] ag = null;
            ag = servicio.GetAgentes();
            Assert.IsNull(ag);
        }

        [TestMethod]
        [TestCategory("GetAgentes()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetAgentes_b()  
        {   //BD conAgentes
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.AgenteWS[] ag = null;
            ag = servicio.GetAgentes();
            Assert.IsNotNull(ag);
        }
        
        

        [TestMethod]
        [TestCategory("ComprobarConexion()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarConexion_a()       
        {
            VPService.IVPService service = new VPService.VPServiceClient();

            Boolean conectado_real = true;

            Assert.AreEqual(service.ComprobarConexion(), conectado_real);
        }

        [TestMethod]
        [TestCategory("ComprobarConexion()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void ComprobarConexion_b()       
        {
            VPService.IVPService service = new VPService.VPServiceClient();

            Boolean conectado_real = false;

            Assert.AreEqual(service.ComprobarConexion(), conectado_real);
        }



        [TestMethod]
        [TestCategory("AddAgente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddAgente_a()   
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.AgenteWS newAgente = new VPService.AgenteWS
            {
                Apellido1 = "Martinez",
                Apellido2 = "Mondejar",
                DNI = "87654321B",
                Email = "pablo_benitez@gmail.com",
                Estado_Civil = "Soltero",
                Nombre = "Pablo",
                NumPlaca = "4321R",
                Rango = "Policia",
                Telefono = "3333",
            };
            bool resultado_esperado = true;
            bool resultado_real = servicio.AddAgente(newAgente);
            Assert.AreEqual(resultado_esperado, resultado_real);
        }

        [TestMethod]
        [TestCategory("AddAgente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddAgente_b()  
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.AgenteWS newAgente = new VPService.AgenteWS
            {
                Apellido1 = "Nieves",
                Apellido2 = "Carretero",
                DNI = null,
                Email = "pablo_benitez@gmail.com",
                Estado_Civil = "Soltero",
                Nombre = "Alvaro",
                NumPlaca = "234R",
                Rango = "Policia",
                Telefono = "3333",
            };
            bool resultado_esperado = false;
            bool resultado_real = servicio.AddAgente(newAgente);
            Assert.AreEqual(resultado_esperado, resultado_real);
        }

        [TestMethod]
        [TestCategory("AddDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddDenuncia_desaparicion()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();

            //FOTO
            VPService.FotoWS image = new VPService.FotoWS();
            System.Drawing.Bitmap bmp = null;
            string current_path = System.IO.Directory.GetCurrentDirectory();
            int i = current_path.IndexOf("VirtualPolice\\");
            string current_file = current_path.Substring(0, i - 1) + "\\VirtualPolice\\VPTestUnitarias\\foto.png";

            bmp = new Bitmap(current_file);
            image.Size = bmp.Size.Height * bmp.Size.Width;
            image.Type = 3; //PNG
            image.Image = imageToByteArray(bmp, image.Type);


            VPService.DesaparicionWS newDesaparicion = new VPService.DesaparicionWS
            {
                Descripcion_fisica = "aaa",
                Foto = image,
                DNI_Desaparecido = "44444444A"
            };
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS
            {
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                Direccion_hecho = "Calle Cid",
                DNI_Denunciante = "55555555A",
                Fecha_hecho = new DateTime(2010, 09, 28),
                Hora_hecho = new TimeSpan(5, 5, 5),
                Naturaleza_lugar_hecho = "aaa",
                NumPlaca = "1234L",
                Tipo = "Desaparicion",
                Desaparicion = newDesaparicion
            };
            
            int resultado_real = servicio.AddDenuncia(newDenuncia);
            int idDesaparicion = resultado_real;
            Assert.AreNotEqual(-1, resultado_real);
            VPService.DenunciaWS denuncia_esperada = servicio.GetDenuncia(resultado_real);
            Assert.AreEqual(newDenuncia.Descripcion_hecho, denuncia_esperada.Descripcion_hecho);
            Assert.AreEqual(newDenuncia.Direccion_hecho, denuncia_esperada.Direccion_hecho);
            Assert.AreEqual(newDenuncia.DNI_Denunciante, denuncia_esperada.DNI_Denunciante);
            Assert.AreEqual(newDenuncia.Naturaleza_lugar_hecho, denuncia_esperada.Naturaleza_lugar_hecho);
            Assert.AreEqual(newDenuncia.NumPlaca, denuncia_esperada.NumPlaca);
            Assert.AreEqual(newDenuncia.Tipo, denuncia_esperada.Tipo);
            Assert.AreEqual(newDenuncia.Desaparicion.Descripcion_fisica, denuncia_esperada.Desaparicion.Descripcion_fisica);
            
            Assert.AreEqual(newDenuncia.Desaparicion.DNI_Desaparecido, denuncia_esperada.Desaparicion.DNI_Desaparecido);
            Assert.AreEqual(image.Size, denuncia_esperada.Desaparicion.Foto.Size);
            Assert.AreEqual(image.Type, denuncia_esperada.Desaparicion.Foto.Type);
            CollectionAssert.AreEqual(image.Image, denuncia_esperada.Desaparicion.Foto.Image);    
        }

        [TestMethod]
        [TestCategory("AddDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddDenuncia_secuestro()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            

            VPService.SecuestroWS newSecuestro = new VPService.SecuestroWS
            {
                DNI_Ciudadano = "55555555A"
            };
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS
            {
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                Direccion_hecho = "Calle Cid",
                DNI_Denunciante = "55555555A",
                Fecha_hecho = new DateTime(2010, 09, 28),
                Hora_hecho = new TimeSpan(5, 5, 5),
                Naturaleza_lugar_hecho = "aaa",
                NumPlaca = "1234L",
                Tipo = "Secuestro",
                Secuestro = newSecuestro
            };

            int resultado_real = servicio.AddDenuncia(newDenuncia);
            Assert.AreNotEqual(-1, resultado_real);
            VPService.DenunciaWS denuncia_esperada = servicio.GetDenuncia(resultado_real);
            Assert.AreEqual(newDenuncia.Descripcion_hecho, denuncia_esperada.Descripcion_hecho);
            Assert.AreEqual(newDenuncia.Direccion_hecho, denuncia_esperada.Direccion_hecho);
            Assert.AreEqual(newDenuncia.DNI_Denunciante, denuncia_esperada.DNI_Denunciante);
            Assert.AreEqual(newDenuncia.Naturaleza_lugar_hecho, denuncia_esperada.Naturaleza_lugar_hecho);
            Assert.AreEqual(newDenuncia.NumPlaca, denuncia_esperada.NumPlaca);
            Assert.AreEqual(newDenuncia.Tipo, denuncia_esperada.Tipo);
            Assert.AreEqual(newDenuncia.Secuestro.DNI_Ciudadano, denuncia_esperada.Secuestro.DNI_Ciudadano);
        }

        [TestMethod]
        [TestCategory("AddDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddDenuncia_roboCoche()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
           

            VPService.RoboCocheWS newRoboCoche = new VPService.RoboCocheWS
            {
                Color = "Negro",
                Marca = "Seat",
                Modelo = "León",
                Matricula = "1289III",
                NumBastidor = "55555555555555555"
            };
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS
            {
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                Direccion_hecho = "Calle Cid",
                DNI_Denunciante = "55555555A",
                Fecha_hecho = new DateTime(2010, 09, 28),
                Hora_hecho = new TimeSpan(5, 5, 5),
                Naturaleza_lugar_hecho = "aaa",
                NumPlaca = "1234L",
                Tipo = "Robo de vehiculo",
                RoboCoche = newRoboCoche
            };

            int resultado_real = servicio.AddDenuncia(newDenuncia);
            Assert.AreNotEqual(-1, resultado_real);
            VPService.DenunciaWS denuncia_esperada = servicio.GetDenuncia(resultado_real);
            Assert.AreEqual(newDenuncia.Descripcion_hecho, denuncia_esperada.Descripcion_hecho);
            Assert.AreEqual(newDenuncia.Direccion_hecho, denuncia_esperada.Direccion_hecho);
            Assert.AreEqual(newDenuncia.DNI_Denunciante, denuncia_esperada.DNI_Denunciante);
            Assert.AreEqual(newDenuncia.Naturaleza_lugar_hecho, denuncia_esperada.Naturaleza_lugar_hecho);
            Assert.AreEqual(newDenuncia.NumPlaca, denuncia_esperada.NumPlaca);
            Assert.AreEqual(newDenuncia.Tipo, denuncia_esperada.Tipo);
            Assert.AreEqual(newDenuncia.RoboCoche.Color, denuncia_esperada.RoboCoche.Color);
            Assert.AreEqual(newDenuncia.RoboCoche.Marca, denuncia_esperada.RoboCoche.Marca);
            Assert.AreEqual(newDenuncia.RoboCoche.Matricula, denuncia_esperada.RoboCoche.Matricula);
            Assert.AreEqual(newDenuncia.RoboCoche.Modelo, denuncia_esperada.RoboCoche.Modelo);
            Assert.AreEqual(newDenuncia.RoboCoche.NumBastidor, denuncia_esperada.RoboCoche.NumBastidor);
        }

        [TestMethod]
        [TestCategory("AddDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddDenuncia_denuncia()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();

            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS
            {
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                Direccion_hecho = "Calle Cid",
                DNI_Denunciante = "55555555A",
                Fecha_hecho = new DateTime(2010, 09, 28),
                Hora_hecho = new TimeSpan(5, 5, 5),
                Naturaleza_lugar_hecho = "aaa",
                NumPlaca = "1234L",
                Tipo = "Denuncia"
            };

            int resultado_real = servicio.AddDenuncia(newDenuncia);
            Assert.AreNotEqual(-1, resultado_real);
            VPService.DenunciaWS denuncia_esperada = servicio.GetDenuncia(resultado_real);
            Assert.AreEqual(newDenuncia.Descripcion_hecho, denuncia_esperada.Descripcion_hecho);
            Assert.AreEqual(newDenuncia.Direccion_hecho, denuncia_esperada.Direccion_hecho);
            Assert.AreEqual(newDenuncia.DNI_Denunciante, denuncia_esperada.DNI_Denunciante);
            Assert.AreEqual(newDenuncia.Naturaleza_lugar_hecho, denuncia_esperada.Naturaleza_lugar_hecho);
            Assert.AreEqual(newDenuncia.NumPlaca, denuncia_esperada.NumPlaca);
            Assert.AreEqual(newDenuncia.Tipo, denuncia_esperada.Tipo);
        }

        [TestMethod]
        [TestCategory("AddDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AddDenuncia_fallo()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();

            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS
            {
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                Direccion_hecho = "Calle Cid",
                DNI_Denunciante = "55555555A",
                Fecha_hecho = new DateTime(2010, 09, 28),
                Hora_hecho = new TimeSpan(5, 5, 5),
                Naturaleza_lugar_hecho = "aaa",
                NumPlaca = null,
                Tipo = "Denuncia"
            };

            int resultado_esperado = -1;
            int resultado_real = servicio.AddDenuncia(newDenuncia);
            Assert.AreEqual(resultado_esperado, resultado_real);
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn, int type)
        {
            MemoryStream ms = new MemoryStream();
            switch (type)
            {
                case 1:
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case 2:
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                default:
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    break;
            }


            return ms.ToArray();
        }


        [TestMethod]
        [TestCategory("GetDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetDenuncia_NoExistente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS();
            newDenuncia = servicio.GetDenuncia(500);
            Assert.IsNull(newDenuncia);
        }

        [TestMethod]
        [TestCategory("GetDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetDenuncia_denuncia()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS();
            VPService.DenunciaWS denuncia_esperada = new VPService.DenunciaWS
            {
                Tipo = "Denuncia",
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                DNI_Denunciante = "55555555A",
                Direccion_hecho = "Calle Cid",
                Naturaleza_lugar_hecho = "aaa",
                Id = 6,
                NumPlaca = "1234L"
            };
            newDenuncia = servicio.GetDenuncia(6);
            Assert.AreEqual(denuncia_esperada.Tipo, newDenuncia.Tipo);
            Assert.AreEqual(denuncia_esperada.Descripcion_hecho, newDenuncia.Descripcion_hecho);
            Assert.AreEqual(denuncia_esperada.DNI_Denunciante, newDenuncia.DNI_Denunciante);
            Assert.AreEqual(denuncia_esperada.Direccion_hecho, newDenuncia.Direccion_hecho);
            Assert.AreEqual(denuncia_esperada.Naturaleza_lugar_hecho, newDenuncia.Naturaleza_lugar_hecho);
            Assert.AreEqual(denuncia_esperada.Id, newDenuncia.Id);
            Assert.AreEqual(denuncia_esperada.NumPlaca, newDenuncia.NumPlaca);
        }

        [TestMethod]
        [TestCategory("GetDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetDenuncia_roboCoche()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS();
            VPService.RoboCocheWS coche_esperado = new VPService.RoboCocheWS
            {
                Color = "Rojo",
                Marca = "Audi",
                Matricula = "1234AB",
                Modelo = "A3",
                NumBastidor = "45342"
            };
            VPService.DenunciaWS denuncia_esperada = new VPService.DenunciaWS
            {
                Tipo = "Robo de vehiculo",
                RoboCoche = coche_esperado,
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                DNI_Denunciante = "55555555A",
                Direccion_hecho = "Calle Cid",
                Naturaleza_lugar_hecho = "aaa",
                Id = 3,
                NumPlaca = "1234L"
            };

            newDenuncia = servicio.GetDenuncia(3);
            Assert.AreEqual(denuncia_esperada.Tipo, newDenuncia.Tipo);
            Assert.AreEqual(denuncia_esperada.Descripcion_hecho, newDenuncia.Descripcion_hecho);
            Assert.AreEqual(denuncia_esperada.DNI_Denunciante, newDenuncia.DNI_Denunciante);
            Assert.AreEqual(denuncia_esperada.Direccion_hecho, newDenuncia.Direccion_hecho);
            Assert.AreEqual(denuncia_esperada.Naturaleza_lugar_hecho, newDenuncia.Naturaleza_lugar_hecho);
            Assert.AreEqual(denuncia_esperada.Id, newDenuncia.Id);
            Assert.AreEqual(denuncia_esperada.NumPlaca, newDenuncia.NumPlaca);
            Assert.AreEqual(denuncia_esperada.RoboCoche.Modelo, newDenuncia.RoboCoche.Modelo);
            Assert.AreEqual(denuncia_esperada.RoboCoche.Marca, newDenuncia.RoboCoche.Marca);
            Assert.AreEqual(denuncia_esperada.RoboCoche.Matricula, newDenuncia.RoboCoche.Matricula);
            Assert.AreEqual(denuncia_esperada.RoboCoche.Color, newDenuncia.RoboCoche.Color);
            Assert.AreEqual(denuncia_esperada.RoboCoche.NumBastidor, newDenuncia.RoboCoche.NumBastidor);
        }

        [TestMethod]
        [TestCategory("GetDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetDenuncia_secuestro()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS();
            VPService.SecuestroWS secuestro_esperado = new VPService.SecuestroWS
            {
                DNI_Ciudadano = "55555555A"
            };
            VPService.DenunciaWS denuncia_esperada = new VPService.DenunciaWS
            {
                Tipo = "Secuestro",
                Secuestro = secuestro_esperado,
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                DNI_Denunciante = "55555555A",
                Direccion_hecho = "Calle Cid",
                Naturaleza_lugar_hecho = "aaa",
                Id = 5,
                NumPlaca = "1234L"
            };
            newDenuncia = servicio.GetDenuncia(5);
            Assert.AreEqual(denuncia_esperada.Tipo, newDenuncia.Tipo);
            Assert.AreEqual(denuncia_esperada.Descripcion_hecho, newDenuncia.Descripcion_hecho);
            Assert.AreEqual(denuncia_esperada.DNI_Denunciante, newDenuncia.DNI_Denunciante);
            Assert.AreEqual(denuncia_esperada.Direccion_hecho, newDenuncia.Direccion_hecho);
            Assert.AreEqual(denuncia_esperada.Naturaleza_lugar_hecho, newDenuncia.Naturaleza_lugar_hecho);
            Assert.AreEqual(denuncia_esperada.Id, newDenuncia.Id);
            Assert.AreEqual(denuncia_esperada.NumPlaca, newDenuncia.NumPlaca);
            Assert.AreEqual(denuncia_esperada.Secuestro.DNI_Ciudadano, newDenuncia.Secuestro.DNI_Ciudadano);
        }

        [TestMethod]
        [TestCategory("GetDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetDenuncia_desaparicion()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            //FOTO
            VPService.FotoWS image = new VPService.FotoWS();
            System.Drawing.Bitmap bmp = null;
            string current_path = System.IO.Directory.GetCurrentDirectory();
            int i = current_path.IndexOf("VirtualPolice\\");
            string current_file = current_path.Substring(0, i - 1) + "\\VirtualPolice\\VPTestUnitarias\\foto.png";

            bmp = new Bitmap(current_file);
            image.Size = bmp.Size.Height * bmp.Size.Width;
            image.Type = 3; //PNG
            image.Image = imageToByteArray(bmp, image.Type);


            VPService.DesaparicionWS desaparicion_esperada = new VPService.DesaparicionWS
            {
                Descripcion_fisica = "aaa",
                Foto = image,
                DNI_Desaparecido = "44444444A"
            };

            VPService.DenunciaWS newDenuncia = new VPService.DenunciaWS();
            newDenuncia = servicio.GetDenuncia(8);
            VPService.DenunciaWS denuncia_esperada = new VPService.DenunciaWS
            {
                Id=8,
                Descripcion_hecho = "Desaparecio en el barrio san pablo",
                Direccion_hecho = "Calle Cid",
                DNI_Denunciante = "55555555A",
                Fecha_hecho = new DateTime(2010, 09, 28),
                Hora_hecho = new TimeSpan(5, 5, 5),
                Naturaleza_lugar_hecho = "aaa",
                NumPlaca = "1234L",
                Tipo = "Desaparicion",
                Desaparicion = desaparicion_esperada
            };

            Assert.AreEqual(denuncia_esperada.Tipo, newDenuncia.Tipo);
            Assert.AreEqual(denuncia_esperada.Descripcion_hecho, newDenuncia.Descripcion_hecho);
            Assert.AreEqual(denuncia_esperada.DNI_Denunciante, newDenuncia.DNI_Denunciante);
            Assert.AreEqual(denuncia_esperada.Direccion_hecho, newDenuncia.Direccion_hecho);
            Assert.AreEqual(denuncia_esperada.Naturaleza_lugar_hecho, newDenuncia.Naturaleza_lugar_hecho);
            Assert.AreEqual(denuncia_esperada.Id, newDenuncia.Id);
            Assert.AreEqual(denuncia_esperada.NumPlaca, newDenuncia.NumPlaca);
            Assert.AreEqual(denuncia_esperada.Desaparicion.Descripcion_fisica, newDenuncia.Desaparicion.Descripcion_fisica);
            
            Assert.AreEqual(denuncia_esperada.Desaparicion.DNI_Desaparecido, newDenuncia.Desaparicion.DNI_Desaparecido);
            Assert.AreEqual(image.Size, newDenuncia.Desaparicion.Foto.Size);
            Assert.AreEqual(image.Type, newDenuncia.Desaparicion.Foto.Type);
            CollectionAssert.AreEqual(image.Image, newDenuncia.Desaparicion.Foto.Image);
        }


        [TestMethod]
        [TestCategory("GetNotificaciones()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void GetNotificaciones_Todas()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.DenunciaWS denuncia_esperada = new VPService.DenunciaWS
            {
                Tipo = "Denuncia",
                Id = 7
            };
            VPService.DenunciaWS[] denuncias_esperadas = new VPService.DenunciaWS[1];
            denuncias_esperadas[0] = denuncia_esperada;
            VPService.DenunciaWS[] denuncias_reales = servicio.GetNotificaciones();
            foreach (VPService.DenunciaWS d in denuncias_reales)
            {
                Assert.AreEqual(denuncias_esperadas[0].Tipo, d.Tipo);
                Assert.AreEqual(denuncias_esperadas[0].Id, d.Id);
            }
        }

        [TestMethod]
        [TestCategory("GetNotificaciones()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetNotificaciones_SinNotificaciones()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.DenunciaWS[] denuncias = servicio.GetNotificaciones();
            Assert.IsNull(denuncias);
        }


        [TestMethod]
        [TestCategory("GetExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetExpediente_NoExistente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = "ASDF";
            VPService.ExpedienteWS exp = new VPService.ExpedienteWS();
            exp = servicio.GetExpediente(DNI);
            Assert.IsNull(exp);
        }

        [TestMethod]
        [TestCategory("GetExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetExpediente_SinFoto()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();

            String DNI = "22222222A";
            double Altura = 0;
            double Peso = 0;
            String Estado_Civil = "aaaaa";
            String Telefono = "0";

            VPService.ExpedienteWS exp = servicio.GetExpediente(DNI);

            Assert.AreEqual(exp.DNI, DNI);
            Assert.AreEqual(exp.Altura, Altura);
            Assert.AreEqual(exp.Peso, Peso);
            Assert.AreEqual(exp.Estado_Civil, Estado_Civil);
            Assert.AreEqual(exp.Telefono, Telefono);
            Assert.IsNull(exp.Foto);
        }

        [TestMethod]
        [TestCategory("GetExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void GetExpediente_ConFoto()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();

            String DNI = "44444444A";
            double Altura = 0;
            double Peso = 0;
            String Estado_Civil = "aaaaa";
            String Telefono = "0";

            //FOTO
            VPService.FotoWS image = new VPService.FotoWS();
            System.Drawing.Bitmap bmp = null;
            string current_path = System.IO.Directory.GetCurrentDirectory();
            int i = current_path.IndexOf("VirtualPolice\\");
            string current_file = current_path.Substring(0, i - 1) + "\\VirtualPolice\\VPTestUnitarias\\foto.png";

            bmp = new Bitmap(current_file);
            image.Size = bmp.Size.Height * bmp.Size.Width;
            image.Type = 3; //PNG
            image.Image = imageToByteArray(bmp, image.Type);

            VPService.ExpedienteWS exp = servicio.GetExpediente(DNI);


            Assert.AreEqual(exp.DNI, DNI);
            Assert.AreEqual(exp.Altura, Altura);
            Assert.AreEqual(exp.Peso, Peso);
            Assert.AreEqual(exp.Estado_Civil, Estado_Civil);
            Assert.AreEqual(exp.Telefono, Telefono);
            Assert.AreEqual(image.Size, exp.Foto.Size);
            Assert.AreEqual(image.Type, exp.Foto.Type);
            CollectionAssert.AreEqual(image.Image, exp.Foto.Image);
        }


        [TestMethod]
        [TestCategory("AsociarExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AsociarExpediente_DenunciaInexistente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = "06284403L";
            int id_Denuncia = 50000;
            bool resultado, resultado_esperado = false;
            resultado = servicio.AsociarExpediente(id_Denuncia, DNI);
            VPService.DenunciaWS denuncia = servicio.GetDenuncia(id_Denuncia);
            Assert.AreEqual(resultado_esperado, resultado);
            Assert.IsNull(denuncia);
        }

        [TestMethod]
        [TestCategory("AsociarExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AsociarExpediente_DenunciaYaAsociada()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = "06284403L";
            int id_Denuncia = 2;
            bool resultado, resultado_esperado = false;
            resultado = servicio.AsociarExpediente(id_Denuncia, DNI);
            VPService.DenunciaWS denuncia = servicio.GetDenuncia(id_Denuncia);
            Assert.AreEqual(resultado_esperado, resultado);
            Assert.AreNotEqual(denuncia.DNI_expediente, DNI);
        }

        [TestMethod]
        [TestCategory("AsociarExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AsociarExpediente_ExpedienteInexistente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = null;
            int id_Denuncia = 1;
            bool resultado, resultado_esperado = false;
            resultado = servicio.AsociarExpediente(id_Denuncia, DNI);
            VPService.DenunciaWS denuncia = servicio.GetDenuncia(id_Denuncia);
            VPService.ExpedienteWS expediente = servicio.GetExpediente(DNI);
            Assert.AreEqual(resultado_esperado, resultado);
            Assert.IsNotNull(denuncia);
            Assert.IsNull(expediente);
            
        }

        [TestMethod]
        [TestCategory("AsociarExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void AsociarExpediente_Correctamente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = "06284403L";
            int id_Denuncia = 1;
            bool resultado, resultado_esperado = true;
            resultado = servicio.AsociarExpediente(id_Denuncia, DNI);
            VPService.DenunciaWS denuncia = servicio.GetDenuncia(id_Denuncia);
            VPService.ExpedienteWS expediente = servicio.GetExpediente(DNI);
            Assert.AreEqual(resultado_esperado, resultado);
            Assert.AreEqual(denuncia.DNI_expediente, DNI);
        }


        [TestMethod]
        [TestCategory("GetDenuncias()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void GetDenuncias_Todas() 
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = null;
            VPService.DenunciaWS[] denuncias = null;
            VPService.DenunciaWS[] denuncias_esperadas = new VPService.DenunciaWS[2];
            denuncias = servicio.GetDenuncias(DNI);
            int i = 0;

            denuncias_esperadas[0] = new VPService.DenunciaWS
            {
                Id = 1,
                Tipo = "Daños",
                Descripcion_hecho = "des",
                Direccion_hecho = "dir",
                DNI_Denunciante = "06284403L",
                Naturaleza_lugar_hecho = "Espacios abiertos",
                NumPlaca = "1234K",
                DNI_expediente = null
            };
            denuncias_esperadas[1] = new VPService.DenunciaWS
            {
                Id = 2,
                Tipo = "Daños",
                Descripcion_hecho = "descr",
                Direccion_hecho = "dir",
                DNI_Denunciante = "06284403K",
                Naturaleza_lugar_hecho = "Espacios abiertos",
                NumPlaca = "1234K",
                DNI_expediente = "06284403K"
            };

            foreach (VPService.DenunciaWS d in denuncias)
            {
                Assert.AreEqual(d.Id, denuncias_esperadas[i].Id);
                Assert.AreEqual(d.Tipo, denuncias_esperadas[i].Tipo);
                Assert.AreEqual(d.Descripcion_hecho, denuncias_esperadas[i].Descripcion_hecho);
                Assert.AreEqual(d.Direccion_hecho, denuncias_esperadas[i].Direccion_hecho);
                Assert.AreEqual(d.Naturaleza_lugar_hecho, denuncias_esperadas[i].Naturaleza_lugar_hecho);
                Assert.AreEqual(d.NumPlaca, denuncias_esperadas[i].NumPlaca);

            
                i++;             
            }


        
        
        }


        [TestMethod]
        [TestCategory("GetDenuncias()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void GetDenuncias_DNInoExistente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = "ASDF";
            VPService.DenunciaWS[] denuncias = null;
            denuncias = servicio.GetDenuncias(DNI);
            Assert.IsNull(denuncias);
            

        }



        [TestMethod]
        [TestCategory("GetDenuncias()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void GetDenuncias_DNIexistente()
        {
            VPService.IVPService servicio = new VPService.VPServiceClient();
            String DNI = "06284403K";
            VPService.DenunciaWS[] denuncias = null;
            denuncias = servicio.GetDenuncias(DNI);
            VPService.DenunciaWS denuncia_esperada = new VPService.DenunciaWS
            {
                Id = 2,
                Tipo = "Daños",
                Descripcion_hecho = "descr",
                Direccion_hecho = "dir",
                DNI_Denunciante = "06284403K",
                Naturaleza_lugar_hecho = "Espacios abiertos",
                NumPlaca = "1234K",
                DNI_expediente = "06284403K"
            };
            Assert.IsNotNull(denuncias);
            foreach (VPService.DenunciaWS d in denuncias)
            {
                Assert.AreEqual(d.Id, denuncia_esperada.Id);
                Assert.AreEqual(d.Tipo, denuncia_esperada.Tipo);
                Assert.AreEqual(d.Descripcion_hecho, denuncia_esperada.Descripcion_hecho);
                Assert.AreEqual(d.Direccion_hecho, denuncia_esperada.Direccion_hecho);
                Assert.AreEqual(d.Naturaleza_lugar_hecho, denuncia_esperada.Naturaleza_lugar_hecho);
                Assert.AreEqual(d.NumPlaca, denuncia_esperada.NumPlaca);
                Assert.AreEqual(d.DNI_expediente, denuncia_esperada.DNI_expediente);
            }


        }


        [TestMethod]
        [TestCategory("DeleteDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void DeleteDenuncia_NoExiste()
        { 

            VPService.IVPService servicio = new VPService.VPServiceClient();

            int idDenuncia = -666;

            Boolean resultado_esperado = false;
            Boolean resultado_real = servicio.DeleteDenuncia(idDenuncia);
            Assert.AreEqual(resultado_esperado, resultado_real);
            Assert.AreEqual(servicio.GetDenuncia(idDenuncia),null);

        }

        [TestMethod]
        [TestCategory("DeleteDenuncia()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]
        public void DeleteDenuncia_Existe()
        { //existe la denuncia

            VPService.IVPService servicio = new VPService.VPServiceClient();

            int idDenuncia = 7;

            Boolean resultado_esperado = true;
            Boolean resultado_real = servicio.DeleteDenuncia(idDenuncia);
            Assert.AreEqual(resultado_esperado, resultado_real);
            Assert.AreEqual(servicio.GetDenuncia(idDenuncia),null);

        }

        [TestMethod]
        [TestCategory("CrearExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void CrearExpedientes_expedienteExistente()
        {
            bool resultadoEsperado = false;
            bool resultadoReal;

            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.ExpedienteWS newExpediente = new VPService.ExpedienteWS
            {

                DNI = "06284403K",
                Altura = 0,
                Peso = 0,
                Edad = 0,
                Estado_Civil = "aaaaa",
                Telefono = "0"

            };

            resultadoReal = servicio.CrearExpediente(newExpediente);
            Assert.AreEqual(resultadoEsperado, resultadoReal);

        }

        [TestMethod]
        [TestCategory("CrearExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void CrearExpedientes_expedienteNoExistenteSinFoto()
        {
            bool resultadoEsperado = true;
            bool resultadoReal;

            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.ExpedienteWS newExpediente = new VPService.ExpedienteWS
            {

                DNI = "22222222A",
                Altura = 0,
                Peso = 0,
                Estado_Civil = "aaaaa",
                Telefono = "0"

            };

            resultadoReal = servicio.CrearExpediente(newExpediente);
            VPService.ExpedienteWS exp_real = servicio.GetExpediente("22222222A");
            Assert.AreEqual(resultadoEsperado, resultadoReal);
            Assert.AreEqual(newExpediente.DNI, exp_real.DNI);
            Assert.AreEqual(newExpediente.Altura, exp_real.Altura);
            Assert.AreEqual(newExpediente.Peso, exp_real.Peso);
            Assert.AreEqual(newExpediente.Estado_Civil, exp_real.Estado_Civil);
            Assert.AreEqual(newExpediente.Telefono, exp_real.Telefono);

        }

        [TestMethod]
        [TestCategory("CrearExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void CrearExpedientes_expedienteNoExistenteConFoto()
        {
            bool resultadoEsperado = true;
            bool resultadoReal;

            VPService.IVPService servicio = new VPService.VPServiceClient();

            //FOTO
            VPService.FotoWS image = new VPService.FotoWS();
            System.Drawing.Bitmap bmp = null;
            string current_path = System.IO.Directory.GetCurrentDirectory();
            int i = current_path.IndexOf("VirtualPolice\\");
            string current_file = current_path.Substring(0, i - 1) + "\\VirtualPolice\\VPTestUnitarias\\foto.png";

            bmp = new Bitmap(current_file);
            image.Size = bmp.Size.Height * bmp.Size.Width;
            image.Type = 3; //PNG
            image.Image = imageToByteArray(bmp, image.Type);

            VPService.ExpedienteWS newExpediente = new VPService.ExpedienteWS
            {

                DNI = "44444444A",
                Altura = 0,
                Peso = 0,
                Estado_Civil = "aaaaa",
                Telefono = "0",
                Foto = image
            };

            resultadoReal = servicio.CrearExpediente(newExpediente);
            VPService.ExpedienteWS exp_real = servicio.GetExpediente("44444444A");
            Assert.AreEqual(resultadoEsperado, resultadoReal);
            Assert.AreEqual(newExpediente.DNI, exp_real.DNI);
            Assert.AreEqual(newExpediente.Altura, exp_real.Altura);
            Assert.AreEqual(newExpediente.Peso, exp_real.Peso);
            Assert.AreEqual(newExpediente.Estado_Civil, exp_real.Estado_Civil);
            Assert.AreEqual(newExpediente.Telefono, exp_real.Telefono);
            Assert.AreEqual(image.Size, exp_real.Foto.Size);
            Assert.AreEqual(image.Type, exp_real.Foto.Type);
            CollectionAssert.AreEqual(image.Image, exp_real.Foto.Image);

        }

        [TestMethod]
        [TestCategory("CrearExpediente()")]
        [TestProperty("CajaBlanca", "CaminoBasico")]

        public void CrearExpedientes_excepcion()
        {
            bool resultadoEsperado = false;
            bool resultadoReal;

            VPService.IVPService servicio = new VPService.VPServiceClient();
            VPService.ExpedienteWS newExpediente = new VPService.ExpedienteWS
            {

                DNI = null,
                Altura = 0,
                Peso = 0,
                Edad = 0,
                Estado_Civil = "aaaaa",
                Telefono = "0"

            };

            resultadoReal = servicio.CrearExpediente(newExpediente);
            Assert.AreEqual(resultadoEsperado, resultadoReal);

        }
    }
}

