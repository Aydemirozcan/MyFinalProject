using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç 
    public interface IResult
    {
        bool Success { get; }      //get sadece okumak için//Success başarılı veya başarısız onun sonucunu verir.
        string Message { get; }    //Başarılı veya başarısız dan sonra mesajı verir.
    }
}
