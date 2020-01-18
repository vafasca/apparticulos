using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Pedido
    {
        public int PedidoId { get; set; }
        public string NombrePedido { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? FechaReserva { get; set; }
        public int? RepuestoIdRepuesto { get; set; }
        public int? ClienteIdCliente { get; set; }
        public int? InventarioIdAppInventario { get; set; }

        public virtual Cliente ClienteIdClienteNavigation { get; set; }
        public virtual AppInventario InventarioIdAppInventarioNavigation { get; set; }
        public virtual Repuesto RepuestoIdRepuestoNavigation { get; set; }
    }
}
