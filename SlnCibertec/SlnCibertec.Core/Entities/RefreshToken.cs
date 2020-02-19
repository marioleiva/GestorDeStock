using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Entities
{
    public class RefreshToken : EntidadBase
    {
        // FK de la tabla users
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public virtual User User { get; set; }
    }
}
