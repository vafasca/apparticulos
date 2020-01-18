using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Auto
    {
        public Auto()
        {
            Repuesto = new HashSet<Repuesto>();
        }

        public int AutoId { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int? Anio { get; set; }

        public virtual ICollection<Repuesto> Repuesto { get; set; }
    }
}
