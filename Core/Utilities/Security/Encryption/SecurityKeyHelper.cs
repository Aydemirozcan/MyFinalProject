using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)     //Buradaki SecurityKey appsetting json daki SecurityKey
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));             //elmizdeki securiryKey i byte array haline getirip,onu simetrik anahtar haline getiriyor.
        }
    }
}
