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
    
    public partial class Agente
    {
        public Agente()
        {
            this.Actividad = new HashSet<Actividad>();
            this.Denuncia = new HashSet<Denuncia>();
        }
    
        public int ID_Agente { get; set; }
        public string NumPlaca { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Rango { get; set; }
        public string Telefono { get; set; }
        public string Estado_Civil { get; set; }
        public System.DateTime Fecha_Creacion { get; set; }
        public string Observaciones { get; set; }
    
        public virtual Ciudadano Ciudadano { get; set; }
        public virtual ICollection<Actividad> Actividad { get; set; }
        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}