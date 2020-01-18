using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Repuesto = new HashSet<Repuesto>();
        }

        public int CategoriaId { get; set; }
        public string NombreCategoria { get; set; }

        public virtual ICollection<Repuesto> Repuesto { get; set; }
    }
}
