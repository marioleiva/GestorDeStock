using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Entities
{
    public class Venta : EntidadBase
    {
        public double Total { get; set; }
        public int ClienteId { get; set; }
        public int UserId { get; set; }
        public Cliente Cliente { get; set; }
        public User Users { get; set; }
        public HashSet<DetalleVenta> Detalles { get; set; }
    }
}
