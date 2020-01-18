using System;
using System.Collections.Generic;

namespace ArticulosWeb.Models
{
    public partial class Anuncio
    {
        public Anuncio()
        {
            AppInventario = new HashSet<AppInventario>();
        }

        public int AnuncioId { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public byte[] FotoAnuncio { get; set; }
        public DateTime? Gestion { get; set; }

        public virtual ICollection<AppInventario> AppInventario { get; set; }
    }
}
