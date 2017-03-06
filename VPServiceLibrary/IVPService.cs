using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace VPServiceLibrary
{
    ///////////////////////////
    // CONTRATO DE SERVICIOS //
    ///////////////////////////

    [ServiceContract]
    public interface IVPService
    {
        [OperationContract]
        int AddDenuncia(DenunciaWS newDenuncia);

        [OperationContract]
        bool DeleteDenuncia(int idDenuncia);

        [OperationContract]
        DenunciaWS GetDenuncia(int idDenuncia);

        [OperationContract]
        DenunciaWS[] GetNotificaciones();

        [OperationContract]
        bool AddAgente(AgenteWS newAgente);

        [OperationContract]
        bool AddCiudadano(CiudadanoWS newCiudadano);

        [OperationContract]
        AgenteWS GetAgente(string numPlaca);

        [OperationContract]
        bool ComprobarPassword(string numPlaca, string pass);

        [OperationContract]
        CiudadanoWS GetCiudadano(string dni);

        [OperationContract]
        AgenteWS[] GetAgentes();

        [OperationContract]
        ActividadWS[] GetPlanning(string numPlaca, DateTime fecha, int dias);

        [OperationContract]
        bool ComprobarCiuAgente(string numPlaca, string DNI);

        [OperationContract]
        bool ComprobarConexion();




        [OperationContract]
        ExpedienteWS GetExpediente(string DNI); //Te devuelve el expediente asociado a ese dni, si es nulo no existe.
        
        [OperationContract]
        bool CrearExpediente(ExpedienteWS expediente); //Crea un expediente, si consigue crearlo devuelve true, sino false y lo asocia a ese DNI.
        
        [OperationContract]
        bool AsociarExpediente(int id_denuncia, string DNI); //Asocia un expediente a una denuncia, devuelve true si se realiza y false sino.

        [OperationContract]
        DenunciaWS[] GetDenuncias(string DNI); //Devuelve todas las denuncias asociadas a un expediente
      }


    ////////////////////////
    // CONTRATOS DE DATOS //
    ////////////////////////

    [DataContract]
    public class CiudadanoWS
    {
        string dni, nombre, apellido1, apellido2, sexo, calle, numero, letra_piso, localidad, provincia, cp, nacionalidad, huella_dactilar;
        DateTime fecha_nacimiento;
        
        [DataMember]
        public string DNI
        {
            get { return dni; }
            set { dni = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [DataMember]
        public string Apellido1
        {
            get { return apellido1; }
            set { apellido1 = value; }
        }

        [DataMember]
        public string Apellido2
        {
            get { return apellido2; }
            set { apellido2 = value; }
        }

        [DataMember]
        public string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        [DataMember]
        public string Calle
        {
            get { return calle; }
            set { calle = value; }
        }

        [DataMember]
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        [DataMember]
        public string Letra_Piso
        {
            get { return letra_piso; }
            set { letra_piso = value; }
        }

        [DataMember]
        public string Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        [DataMember]
        public string Provincia
        {
            get { return provincia; }
            set { provincia = value; }
        }

        [DataMember]
        public string CP
        {
            get { return cp; }
            set { cp = value; }
        }

        [DataMember]
        public string Nacionalidad
        {
            get { return nacionalidad; }
            set { nacionalidad = value; }
        }

        [DataMember]
        public string Huella_Dactilar
        {
            get { return huella_dactilar; }
            set { huella_dactilar = value; }
        }

        [DataMember]
        public DateTime Fecha_Nacimiento
        {
            get { return fecha_nacimiento; }
            set { fecha_nacimiento = value; }
        }
    }

    [DataContract]
    public class AgenteWS
    {
        string dni, nombre, apellido1, apellido2, numPlaca, email, rango, telefono, estado_civil, observaciones;

        [DataMember]
        public string DNI
        {
            get { return dni; }
            set { dni = value; }
        }

        [DataMember]
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        [DataMember]
        public string Apellido1
        {
            get { return apellido1; }
            set { apellido1 = value; }
        }

        [DataMember]
        public string Apellido2
        {
            get { return apellido2; }
            set { apellido2 = value; }
        }

        [DataMember]
        public string NumPlaca
        {
            get { return numPlaca; }
            set { numPlaca = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public string Rango
        {
            get { return rango; }
            set { rango = value; }
        }

        [DataMember]
        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        [DataMember]
        public string Estado_Civil
        {
            get { return estado_civil; }
            set { estado_civil = value; }
        }

        [DataMember]
        public string Observaciones
        {
            get { return observaciones; }
            set { observaciones = value; }
        }
    }

    [DataContract]
    public class ActividadWS
    {
        string tipo, descripcion;
        DateTime fecha;
        TimeSpan hora_inicio, hora_fin;

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [DataMember]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        [DataMember]
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        [DataMember]
        public TimeSpan Hora_Inicio
        {
            get { return hora_inicio; }
            set { hora_inicio = value; }
        }

        [DataMember]
        public TimeSpan Hora_Fin
        {
            get { return hora_fin; }
            set { hora_fin = value; }
        }
    }
    
    [DataContract]
    public class FotoWS
    {
        int id;
        byte[] image;
        long size;
        short type;

        [DataMember]
        public int Id_Foto
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

        [DataMember]
        public long Size
        {
            get { return size; }
            set { size = value; }
        }

        [DataMember]
        public short Type
        {
            get { return type; }
            set { type = value; }
        }

    }

    [DataContract]
    public class DesaparicionWS
    {
        string descripcion_fisica, dni_desaparecido;
        FotoWS foto;

        [DataMember]
        public string DNI_Desaparecido
        {
            get { return dni_desaparecido; }
            set { dni_desaparecido = value; }
        }


        [DataMember]
        public FotoWS Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        [DataMember]
        public string Descripcion_fisica
        {
            get { return descripcion_fisica; }
            set { descripcion_fisica = value; }
        }

    }

    [DataContract]
    public class SecuestroWS
    {
        string dni_ciudadano;

        [DataMember]
        public string DNI_Ciudadano
        {
            get { return dni_ciudadano; }
            set { dni_ciudadano = value; }
        }
        
    }

    [DataContract]
    public class RoboCocheWS
    {
        string matricula, marca, modelo, color, numBastidor;

        [DataMember]
        public string Matricula
        {
            get { return matricula; }
            set { matricula = value; }
        }

        [DataMember]
        public string Marca
        {
            get { return marca; }
            set { marca = value; }
        }

        [DataMember]
        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        [DataMember]
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        [DataMember]
        public string NumBastidor
        {
            get { return numBastidor; }
            set { numBastidor = value; }
        }
        
    }

    [DataContract]
    public class DenunciaWS
    {
        int id;
        string num_placa,tipo, dni_denunciante, direccion_hecho, naturaleza_lugar_hecho, descripcion_hecho, dni_expediente;
        DateTime fecha_hecho, fecha_creacion;
        TimeSpan hora_hecho;
        DesaparicionWS desaparicion;
        RoboCocheWS robocoche;
        SecuestroWS secuestro;

        [DataMember]
        public DesaparicionWS Desaparicion
        {
            get { return desaparicion; }
            set { desaparicion = value; }
        }

        [DataMember]
        public SecuestroWS Secuestro
        {
            get { return secuestro; }
            set { secuestro = value; }
        }

        [DataMember]
        public RoboCocheWS RoboCoche
        {
            get { return robocoche; }
            set { robocoche = value; }
        }

        [DataMember]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string NumPlaca 
        {
            get { return num_placa; }
            set { num_placa = value; }
        }

        [DataMember]
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        [DataMember]
        public string DNI_Denunciante
        {
            get { return dni_denunciante; }
            set { dni_denunciante = value; }
        }

        [DataMember]
        public string Direccion_hecho
        {
            get { return direccion_hecho; }
            set { direccion_hecho = value; }
        }

        [DataMember]
        public string Naturaleza_lugar_hecho
        {
            get { return naturaleza_lugar_hecho; }
            set { naturaleza_lugar_hecho = value; }
        }

        [DataMember]
        public string Descripcion_hecho
        {
            get { return descripcion_hecho; }
            set { descripcion_hecho = value; }
        }

        [DataMember]
        public DateTime Fecha_hecho
        {
            get { return fecha_hecho; }
            set { fecha_hecho = value; }
        }

        [DataMember]
        public DateTime Fecha_creacion
        {
            get { return fecha_creacion; }
            set { fecha_creacion = value; }
        }

        [DataMember]
        public TimeSpan Hora_hecho
        {
            get { return hora_hecho; }
            set { hora_hecho = value; }
        }

        [DataMember]
        public string DNI_expediente
        {
            get { return dni_expediente; }
            set { dni_expediente = value; }
        }
    }

    [DataContract]
    public class ExpedienteWS
    {
        string dni, estado_civil, telefono;
        DateTime fecha_creacion, fecha_ultimaModif;
        int edad;
        double peso, altura;
        FotoWS foto;
        


        [DataMember]
        public string DNI
        {
            get { return dni; }
            set { dni = value; }
        }

        [DataMember]
        public double Altura
        {
            get { return altura; }
            set { altura = value; }
        }


        [DataMember]
        public double Peso
        {
            get { return peso; }
            set { peso = value; }
        }

        [DataMember]
        public FotoWS Foto
        {
            get { return foto; }
            set { foto = value; }
        }

        [DataMember]
        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }

        [DataMember]
        public string Estado_Civil
        {
            get { return estado_civil; }
            set { estado_civil = value; }
        }
       
        [DataMember]
       public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }
       
        [DataMember]
       public DateTime Fecha_Creacion
       {
           get { return fecha_creacion; }
           set { fecha_creacion = value; }
       }
        
    
        [DataMember]
       public DateTime Fecha_UltimaModif
       {
           get { return fecha_ultimaModif; }
           set { fecha_ultimaModif = value; }
       }
    }
}
