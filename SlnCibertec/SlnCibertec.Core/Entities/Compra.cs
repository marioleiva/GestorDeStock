using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Entities
{
    public class Compra : EntidadBase
    {

        public int Cantidad { get; set; }
        public int ProveedorId { get; set; }
        public int ProductoId { get; set; }
        public Proveedor Proveedores { get; set; }
        public Producto Productos { get; set; }
    }
}
