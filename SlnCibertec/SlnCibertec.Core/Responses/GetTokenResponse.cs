using System;
using System.Collections.Generic;
using System.Text;

namespace SlnCibertec.Core.Responses
{
    public class GetTokenResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
