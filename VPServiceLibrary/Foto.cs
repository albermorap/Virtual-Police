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
    
    public partial class Foto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public long Size { get; set; }
        public short Type { get; set; }
    
        public virtual Expediente Expediente { get; set; }
        public virtual Desaparicion Desaparicion { get; set; }
    }
}