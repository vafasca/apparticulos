using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedido = new HashSet<Pedido>();
        }

        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? Celular { get; set; }
        public int? Ci { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}
