using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics)   //siz params verdiğiniz zaman Run içerisinie istediğiniz kadar IResult verebiliyosunuz parametre olarak//Burada logic iş kuralı demek
        {                                                     //C# arka planda gönderdiğiniz bütün parametreleri(virgülle ayırdığınız) alıp IResult[] Arrayine atıyor.(virgülle istediğiniz kadar IResult parametresi geçebilirsiniz demek oluyor)
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;                          //ErrorResult döner  //Parametre ile gönderdiğimiz iş kurallarından başarısız olanı Business a haber veriyoruz.//Başarısız olan kural döner
                }                                          //Bu yapıda mevcut bir hata varsa direk o hatayı döndürür.
            }
            return null;                                      //Başarılıysa hiçbirşey döndürmesine gerek yok.
        }
    }
}
 