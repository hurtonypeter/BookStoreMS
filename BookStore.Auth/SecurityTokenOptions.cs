using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Auth
{
    public class SecurityTokenOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public EncryptingCredentials EncryptingCredentials { get; set; }
    }
}
