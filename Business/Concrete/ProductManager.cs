using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;                     //Yıldızlı Not: Bir EntityManager (ProjectManager) kendisi hariç başka Dal 'ı enjekte edemez(Yani ProductManager ' a yalnızca IProductDal enjekte edilir.ICategoryDal olamaz) 
using System.Linq;                                    //Bunun yerine başka bir service 'i enjekte ediyoruz.(ICustomerService olabilir)
using System.Text;

namespace Business.Concrete                  //Eğer Manager görürseniz bu iş katmanının somut sınıfı 
{
    public class ProductManager:IProductService                    //Bir iş sınıfı başka sınıfları newlemez.
    {                                                              //Constructor Injection yapıyoruz.
        IProductDal _productDal;                                    //IProductDal ı buraya constaructor yapısı olarak getirdik çünkü IProductDal DataAccess'in soyut nesnesidir.Ve Inmemory ,EntityFramework ile referansı vardır. 
                                                                    //Yani bu işlemi yaparak IProductDal ın içindeki metotları kullanabilir hale geldik.
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("Product.Add")]
        [ValidationAspect(typeof(ProductValidator))]              //Add metodunu doğrula ProductValidator daki kuralları kullanarak.
        public IResult Add(Product product)
        {

            //Business codes
            //validation                                         //eklenmesini istediğiniz nesne nin yapısı ile ilgili doğrulamayı yapan kodlar validation kodlarıdır.

            //if (product.ProductName.Length<2)
            //{
            //    //magic strings
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),CheckIfCategoryLimitExceded());                                //ileride bir kural eklenirse aşağıya kuralı ekleyip, burayada virgül deyip kuralı yanına yazman yeterli.

            if (result != null)
            {
                return result;
            }
             _productDal.Add(product);                                                  //Add in başına void i silip IResult yazdığımız için artık bir şey dönmek zorundayız.Ancak  _productDal.Add(product); buradaki Add kendisi bir void olduğu için başına return yazamıyoruz.
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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult(Messages.Updated);
        }
                                                                                                    //Bir kataegoride 15 den fazla ürün olması kuralı
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)                       //Sadece bu class da çalışsın istediğimiz için private yaptık.//   Kategrideki ürün sayısının kurallara uygunluğunu doğrula.
        {
            //Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();                                                         //SuccessResult 'ı boş geçmemizin sebebi gidip kullanıcıya şu kurallardan geçtide başarılı oldu demeyiz.
        }


        private IResult CheckIfProductNameExists(string productName)                           //Eklenmek istenen ürünün aynı isminde başka bir ürün olmaması kuralı.
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()                          //Sistemdeki toplam Catergory sayısı 15 i aşamaz.//Bu CategoryManager in içinde olabilirdi.Ama biz burada Product için CategoryService i nasıl yorumlanıyor onu göstermek istedik. 
        {                                                                      //Bu durum CategoryService i kullanan bir ürünün bunu nasıl ele aldığıyla alakalı.
            
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();                                                         

        }


    }
}
