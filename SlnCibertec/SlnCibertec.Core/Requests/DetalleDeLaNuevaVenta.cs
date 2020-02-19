using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Requests
{
    public class DetalleDeLaNuevaVenta
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public double Total { get; set; }
    }
}
