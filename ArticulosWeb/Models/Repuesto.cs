using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Repuesto
    {
        public Repuesto()
        {
            Galeria = new HashSet<Galeria>();
            Pedido = new HashSet<Pedido>();
        }

        public int RepuestoId { get; set; }
        public string Nombre { get; set; }
        public int? Precio { get; set; }
        public string Descripcion { get; set; }
        public int? Disponible { get; set; }
        public int? AutoIdAuto { get; set; }
        public int? CategoriaIdCategoria { get; set; }
        public int? InventarioIdAppInventario { get; set; }

        public virtual Auto AutoIdAutoNavigation { get; set; }
        public virtual Categoria CategoriaIdCategoriaNavigation { get; set; }
        public virtual AppInventario InventarioIdAppInventarioNavigation { get; set; }
        public virtual ICollection<Galeria> Galeria { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
