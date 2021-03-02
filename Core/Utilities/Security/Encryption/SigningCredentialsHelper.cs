using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)    //Credential :bir sisteme girebilmek için elimizde olanlardır.(Kullanıcı Adı Parola)
        {
            return new SigningCredentials(securityKey ,SecurityAlgorithms.HmacSha512Signature);           //Anahtar olarak securityKeyi kullan,Şifreleme olarak güvenlik algoritmalarından da HmacSha512Signature yi kullan
        }
    }
}
