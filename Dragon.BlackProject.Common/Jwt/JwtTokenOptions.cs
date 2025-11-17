using System;
using System.Collections.Generic;
using System.Text;

namespace Dragon.BlackProject.Common.Jwt
{
    public class JwtTokenOptions
    {
        public int Type { get; set; } = 0; // 0: HMAC, 1: RSA
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;   
        public string SecretKey { get; set; } = string.Empty;
    }
}
