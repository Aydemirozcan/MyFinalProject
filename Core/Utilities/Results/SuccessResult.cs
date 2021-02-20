using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message) : base(true, message)                //base demek base e birşey gönder demektir.base de burada Result oluyor.//Bu satır sadece mesaj bilgisi gönderir.
        { 
          
        
        }
        public SuccessResult() : base(true)         //Mesaj bilgisi vermek istemiyorsak bunu yaparız.base in tek parametreli olan constructor unu çalıştırır.
        {

        }
    }

    
}
