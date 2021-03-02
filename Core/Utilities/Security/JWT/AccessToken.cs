using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken                  //Erişim anahtarı//Anlamsız karakterlerden oluşan anahtar değeridir.
    {
        public string Token { get; set; }        //Json Web Token dır.
        public DateTime Expiration { get; set; }     //Bitiş zamanını verdiğimiz değer
    }
}
