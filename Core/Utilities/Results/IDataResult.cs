using System;
using System.Collections.Generic;
using System.Text;                                             //IDataResult :hem işlem sonucunu hem mesajı hemde List<Product> gibi dödüreceği şeyi içeren bir yapı görevi görecek.

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult                    //IResult zaten işlem sonucu ve mesajı içerdiği için bir daha yazmadık.Bu şekilde yaptık. 
    {
        T Data { get; }                                       //başarılı -başarısız(true false ) veya mesaj bilgisi dışında senin bir de yukarıda belirttiğim T türünde bir datan olacak.Bu data da ürün olur , ürünler olur.
    }
}
