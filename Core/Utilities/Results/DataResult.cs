using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data,bool success,string message):base(success,message)     //Zaten base inde success ve message bilgisi çalıştırıldığı için burada bir daha yapmamak için base gönderdik.
        {
            Data = data;
        }

        public DataResult(T data,bool success):base(success)                            //Mesajı göndermek istemezse diye yazdık.
        {
            Data = data;
        }
        public T Data { get; }
    }
}
