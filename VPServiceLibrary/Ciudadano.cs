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
    
    public partial class Ciudadano
    {
        public Ciudadano()
        {
            this.Desaparicion = new HashSet<Desaparicion>();
            this.Secuestro = new HashSet<Secuestro>();
            this.Vehiculo = new HashSet<Vehiculo>();
            this.Denuncia = new HashSet<Denuncia>();
        }
    
        public int ID_Ciudadano { get; set; }
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public System.DateTime Fecha_Nacimiento { get; set; }
        public string Sexo { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Letra_Piso { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string CP { get; set; }
        public string Nacionalidad { get; set; }
        public string Huella_Dactilar { get; set; }
    
        public virtual Agente Agente { get; set; }
        public virtual Expediente Expediente { get; set; }
        public virtual ICollection<Desaparicion> Desaparicion { get; set; }
        public virtual ICollection<Secuestro> Secuestro { get; set; }
        public virtual ICollection<Vehiculo> Vehiculo { get; set; }
        public virtual ICollection<Denuncia> Denuncia { get; set; }
    }
}
