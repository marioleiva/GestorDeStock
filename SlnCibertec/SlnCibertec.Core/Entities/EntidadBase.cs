using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Entities
{
    public class EntidadBase
    {
        // PK de la tabla
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
