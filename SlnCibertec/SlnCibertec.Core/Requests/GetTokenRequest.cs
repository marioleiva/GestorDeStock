using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Requests
{
    public class GetTokenRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; }        
    }
}
