using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message) : base(false, message)                //base demek base e birşey gönder demektir.base de burada Result oluyor.//Bu satır sadece mesaj bilgisi gönderir.
        {


        }
        public ErrorResult() : base(false)         //Mesaj bilgisi vermek istemiyorsak bunu yaparız.base in tek parametreli olan constructor unu çalıştırır.
        {

        }
    }
}
