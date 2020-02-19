using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Entities
{
    public class User : EntidadBase
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Roles { get; set; }
        public string Dni { get; set; }
        public string Password { get; set; }
        public HashSet<Venta> Ventas { get; set; }
    }
}
