using DataAccess.Abstract;              
using Entities.Concrete;                           //Concrete içerisinde 2 tane farklı klasör oluşturduk(EntityFramework, InMemory adında)
using Entities.DTOs;
using System;                                     //Çünkü alternatifi olan bir teknoloji üzerinde çalışıyorsanız,Klasörleme teknolojisini kullanırsınız.
using System.Collections.Generic;                //Başka bir zaman EntityFramework klasörüne çalışma yaptığımızda InMemory çalışmasında bir bozulma olmayacaktır.
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{                                                    //InMemoryProductDal: Bellek üzerinde ürün ile ilgili veri erişim kodlarının yazılacağı yerdir.
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;                    //Naming Covention  _( altçizgi ile verilmesinin sebebi class içerisinde diğer metotların dışında yazdık.Bu tür değişkenlere "global değişken" deniyor)
        public InMemoryProductDal()                 //Constructor:Bellekte referans aldığı zaman çalışacak bloktur
        { 

                                                    //Oracle , Sql Server , Postgres ,MongoDb den veri geliyomuş gibi simülasyon yaptık.
            _products = new List<Product> { 
              new Product{ProductId=1,CategoryId=1,ProductName="Bardak",UnitPrice=15,UnitsInStock=15},
              new Product{ProductId=2,CategoryId=1,ProductName="Kamera",UnitPrice=500,UnitsInStock=3},
              new Product{ProductId=3,CategoryId=2,ProductName="Telefon",UnitPrice=1500,UnitsInStock=2},
              new Product{ProductId=4,CategoryId=2,ProductName="Klavye",UnitPrice=150,UnitsInStock=65},
              new Product{ProductId=5,CategoryId=2,ProductName="Fare",UnitPrice=85,UnitsInStock=1},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {

                                                             //LINQ -Language Integrated Query (Dile gömülü sorgulama)


            //Product productToDelete = null;                //Eğer LINQ olmasaydı bu yöntemle silme işlemi yapacaktık.
            // foreach(var p in _products)
            //{
            //  if(product.ProductId == p.ProductId)
            //{
            //   productToDelete = p;
            //}
            //}
            //  _products.Remove(productToDelete);
            
            
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId ==product.ProductId);

                _products.Remove(productToDelete);

                                                               //SingleOrDefault  bir metotdur.Tek bir eleman bulmaya yarar.
                                                              //Normalde direk yazınca gelmez,Ampulden using System.Linq e tıkladık o şekilde kırmızılığı gitti.
                                                             //Lambda   (  =>  ) işaretidir.
                                                            //  SingleOrDefault(p=>    Bu kod foreach ile dönme ile aynı işi yapar.Her p için demektir.
                                                           //Genelde ID aramalarında SingleOrDefault kullanılır bunun yerine FirstOrDefault veya First kullansak da olur. 
                                                          //Eğer SingerOrDefault sorgusu sonucunda 2 tane sonuç gelirse hata verir.Bu yüzden ID bazlı yapılarda kullanılır.
        }                                                //FirstOrDefault kullandığınızda örneğin bir Id sorgulamada eğer birden fazla sonuç veriyorsa ilk sonucu veriyor.

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();         //Where koşulu içindeki şarta uyan bütün elemanları yeni bir liste haline getirir ve onu döndürür.

        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün id'sine sahip olan listedeki ürün id'sini bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId); 
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            product.UnitsInStock = product.UnitsInStock;
        }

        
    }
}
