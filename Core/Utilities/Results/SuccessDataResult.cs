using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results                                             
{
    public class SuccessDataResult<T>:DataResult<T>                                          //Bu sistemi kullanıcak kullanıcıya bir çok imkan sunduk.
    {
        public SuccessDataResult(T data,string message):base(data,true,message)            //işlem sonucunu default true vermiş olduk.
        {
                                                                                          //Anlamı: istersen data ve mesaj ver.
        }

        public SuccessDataResult(T data):base(data,true)
        {                                                                                 //Anlamı: istersen sadece data ver. 

        }

        public SuccessDataResult(string message):base(default,true,message)            //Data yı default haliyle döndürmek istiyebilir ama çok az kullanacağımız bir versiyon bu. 
                                                                                      //Default yazmamızın sebebi çalıştırdığımız T nin default u yani sen bir şey yazmak istemiyorsundur.Bir şey döndürmek istemiyorsan default haline döndürmek istiyorsundur.
        {
                                                                                     //Anlamı: istersen sadece mesaj ver. 
        }


        public SuccessDataResult():base(default,true)                 //Bu versiyon da hiç birşey vermek istemiyoruz mesaj da yok datamız default ve başarı durumu true
        {                                                             //Bu versiyonu da çok kullanmayız.

        }                                                            //Anlamı: istersen hiçbirşey verme. 
    }
}
