using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SlnCibertec.Core.Requests
{
    public class NuevaVenta
    {
        [Range(1, int.MaxValue, ErrorMessage = "No es un Id de Vendedor valido")]
        public int IdVendedor { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "No es un Id de Cliente valido")]
        public int IdCliente { get; set; }
        [Range(1, Double.MaxValue, ErrorMessage = "No es un monto valido")]
        public double Total { get; set; }
        public IEnumerable<DetalleDeLaNuevaVenta> Detalles { get; set; }
    }
}
