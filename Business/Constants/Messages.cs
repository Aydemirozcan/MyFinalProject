using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants                                   //Temel mesajlarımızı bu class içersine koyacağız.Bu class Nortwind projemize ait bu sebepten Core a yazmadık.Core a evrensel kodlar yazılır.
{
   public static class Messages                  //Başında static vermemizin sebebi bu bir sabit ve sürekli new lenmesini istemiyoruz.Başında static olunca direk Messages. diyeceğiz ve uygulama hayatı boyunca tek (intıns)ı oluyor
    {
        public static string ProductAdded = "Ürün eklendi";                    //Bu 2 satuırdaki değişkenleri pascal case yazdık çünkü başında public var.
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime= "Sistem Bakımda";
        public static string ProductsListed= "Ürünler Listelendi";
    }
}
