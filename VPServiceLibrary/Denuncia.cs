//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VPServiceLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Denuncia
    {
        public int ID_Denuncia { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public System.DateTime Fecha_Creacion { get; set; }
        public string Direccion_Hecho { get; set; }
        public string Naturaleza_Lugar_Hecho { get; set; }
        public System.DateTime Fecha_Hecho { get; set; }
        public System.TimeSpan Hora_Hecho { get; set; }
        public string Decripcion_Hecho { get; set; }
        public Nullable<int> ExpedienteID_Expediente { get; set; }
        public int AgenteID_Agente { get; set; }
        public int CiudadanoID_Denunciante { get; set; }
    
        public virtual Expediente Expediente { get; set; }
        public virtual Agente Agente { get; set; }
        public virtual Ciudadano Denunciante { get; set; }
    }
}
