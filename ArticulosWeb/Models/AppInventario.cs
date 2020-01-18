using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class AppInventario
    {
        public AppInventario()
        {
            Pedido = new HashSet<Pedido>();
            Repuesto = new HashSet<Repuesto>();
        }

        public int InventarioId { get; set; }
        public DateTime? Gestion { get; set; }
        public int? AnuncioIdAnuncio { get; set; }

        public virtual Anuncio AnuncioIdAnuncioNavigation { get; set; }
        public virtual ICollection<Pedido> Pedido { get; set; }
        public virtual ICollection<Repuesto> Repuesto { get; set; }
    }
}
