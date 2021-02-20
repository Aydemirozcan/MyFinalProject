using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult                                           //Result ı biz voidlerin yerine yazmış olduk.
    {
        public Result(bool success,string message):this(success)           
        {                                                                 //C# da this demek classın kendisidir(Result).Resultın tek parametreli olan constructor ına success i yolla demek.
            Message = message;                                           //Bunu dediğimizde yazdığımız her iki constructor da çalışır
        }


        public Result(bool success)                           //Sadece başarılı başarısız bilgisi çalıştırmak istiyorsak bunu yaptık.
        {
            Success = success;
        }                                                   //Bu yaptığımız constructorun base ler ile çalışmasına iyi bir örnektir.

        public bool Success { get; }      //Buralarda yaptığımız Getır lar yukarıdaki constroctur içerisinde set edilebilir.

        public string Message { get; }
    }
}
