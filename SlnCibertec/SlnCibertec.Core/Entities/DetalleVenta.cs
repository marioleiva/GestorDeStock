using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Entities
{
    public class DetalleVenta : EntidadBase
    {
        public int Cantidad { get; set; }
        public double SubTotal { get; set; }
        public int ProductoId { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
        public Producto Producto { get; set; }
    }
}
