using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
                                           //Not: Bir interface in kendisi public değildir.Operasyonları publictir.
namespace DataAccess.Abstract
{                                        //Eğer iş yapan bir class ın interface si yoksa hemen ona bir inteface oluştur.
    public interface IProductDal :IEntityRepository<Product>                                                                     //Dal = Data access layer , DAO= Data access object  
    {                                                                                                                           //Veritabanında product ile ilgili operasyonları içeren interface 
        List<ProductDetailDto> GetProductDetails();      //Bu kod sadece porduct için olduğu için buraya yazdık.                                                            //--(Referans verme)--//Bunu yazdıktan sonra ampülden Add reference to 'Entities'  e tıkladık.Ama olmadı çünkü Visual Studio daki bug dan dolayı bu otomatik referance verme işlemini gerçekleştirmedi.Normalde bug olmasaydı otomatik olarak hem referansı verir hem de using e ekler.
                                                                                                                             //Yukardaki satırdakilerin yerine Data Access e sağ tıkladık Add e tıklayıp Project Referance ye tıkladık.Ve ordan Entities katmanı ile bağlantı kurduk.Bir katman ile diğerini bu şekilde bağlarız.
                                                                                                                            //Artık başka bir katmanı referans verdiğimiz için product ın üzerine tıkladığımızda ampulden "Using Entities.Concrete" e tıklıyabiliyoruz. 
                                                                                                                           //Ürünleri kategoriye göre filtrele


    }
}

//Code Refactoring : Kodun iyileştirilmesi