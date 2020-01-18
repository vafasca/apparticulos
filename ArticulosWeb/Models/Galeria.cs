using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Galeria
    {
        public int GaleriaId { get; set; }
        public string NombreFoto { get; set; }
        public byte[] Foto { get; set; }
        public string DescripcionFoto { get; set; }
        public int? RepuestoIdRepuesto { get; set; }

        public virtual Repuesto RepuestoIdRepuestoNavigation { get; set; }
    }
}
