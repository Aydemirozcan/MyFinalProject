using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete                  //Eğer Manager görürseniz bu iş katmanının somut sınıfı 
{
    public class ProductManager:IProductService                    //Bir iş sınıfı başka sınıfları newlemez.
    {                                                              //Constructor Injection yapıyoruz.
        IProductDal _productDal;                                    //IProductDal ı buraya constaructor yapısı olarak getirdik çünkü IProductDal DataAccess'in soyut nesnesidir.Ve Inmemory ,EntityFramework ile referansı vardır. 
                                                                     //Yani bu işlemi yaparak IProductDal ın içindeki metotları kullanabilir hale geldik.
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]              //Add metodunu doğrula ProductValidator deki kurallara göre
        public IResult Add(Product product)
        {
            //Business codes
            //validation                                         //eklenmesini istediğiniz nesne nin yapısı ile ilgili doğrulamayı yapan kodlar validation kodlarıdır.
            
            //if (product.ProductName.Length<2)
            //{
            //    //magic strings
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}
            
            //Business codes
            _productDal.Add(product);                                        //Add in başına void i silip IResult yazdığımız için artık bir şey dönmek zorundayız.Ancak  _productDal.Add(product); buradaki Add kendisi bir void olduğu için başına return yazamıyoruz.
            return new SuccessResult(Messages.ProductAdded);                          //Bu yüzden buraya return yazdık ve Result ın içindekileri döndürüyor.
        }

        public IDataResult<List<Product>>GetAll()
        {
            //iş kodları
            //Yetkisi var mı? gibi soruları ve kuralları geçerse  aşağıdaki kodları çalıştırır yani ürünleri şartı sağlarsa verir

            if (DateTime.Now.Hour==1)                                                                                //DateTime.Now.Hour bizim sistemimizin anlık saatini gösterir.//Bu kodda 22.00 dan 22.59 a kadar
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);                                  //Data döndürmüyoruz sadece mesaj verdik Prdo
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);           

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));                     //SuccessDataResult içinde Product var.Onun da ctor una   _productDal.Get(p => p.ProductId == productId) gönderiryorsun.
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
    }
}
