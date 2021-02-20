using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]                                                //Bu isteği yaparken insanlar bize nasıl ulaşsın.
    [ApiController]                                                           //ATTRIBUTE(C#)  :Bu class bir controllerdır buna ona göre davran demek  // ANNOTATION(JAVA)
    public class ProductsController : ControllerBase
    {
        //Loosely coupled :Gevşek bağlılık yani bir bağımlılık var ama bu soyuta olan bağlılık
        //naming convention
        //IoC Container --Inversion of Control =Değişimin kontrolü    ==Bunu bir kutu gibi bir liste gibi düşünülebilir.Buradaki IProductService in içindeki somut sınıfları newleyip onun içerisine koyarız.//IoC yi hertürlü projede kullanabiliriz.
        IProductService _productService;                                 //Bu field ların default u private 'dır.//C# ve .Net projelerinde this çok fazla tercih edilmez bunun yerine _(Alt tire) kullanmayı tercih ederiz. 
                                                                         //Hiç bir katman diğerinin somut sınıfını kullanamaz.Soyutunu kullanır.(İnterface vb.)
        public ProductsController(IProductService productService)        //Controller sen bir IProductService bağımlılısın  diyor.
        {
            _productService = productService;                           //Buralarda getirip ProductManageri newlemek istemediğimiz için IoC yaptık.Eğer newlwersek ProducManager a bağımlı olurduk.
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {   //Gerçek hayatta API yi geliştirenler bu API size ne zaman ne veriyo ile ilgili dökümantason sağlarlar.Bunun için hazır dökümantasyon imkanı sunan Swagger gibi ürünler vardır.DevArchitecture da default implemente edilmiş olarak gelir.
            //Dependency chain--Bağımlılık zinciri    ( IProductService bir ProductManager' a ihtiyaç duyuyor oda bir EfProductDal a ihiyaç duyuyor.)
            
            var result = _productService.GetAll();                                                  //Object tüm veri tiplerinin atasıdır.Tüm veri tipleri yazılabilir.
            if (result.Success)                                        //Eğer başarı durumu true ise demek çünkü bool un default u true dir.
            {
                return Ok(result);                                    //Postman de 200 Ok koduyla aynı yani 200 döndür demek.200 Ok default Http yanıtımızdır.Hiçbir sorun vermediğinde çalışır.400 Ok olsaydı önce sisteme login olması gerekecekti.
            }
            return BadRequest(result);                               //400 Bad Request sonucu null döner.Bir hata olduğu anlamındadır.
        }
        [HttpGet("getbyıd")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);                                      //Get demek sen bana birşeyler(data olabilir) ver demektir.Ama Post demek ben sana birşeyler vericem demektir. 
            }
            return BadRequest(result);
        }







        [HttpPost("add")]                                       //silme ve güncelleme içinde HttpPost kullanılabilir.(Sektörde%99.9 bu şekilde kullanılır.)
        public IActionResult Add(Product product)               //Ama güncelleme için HttpPut , silme için HttpDelete de kullanılabilir.
        {
            var result = _productService.Add(product);
            if (result.Success)                                        
            {
                return Ok(result);                                      
            }
            return BadRequest(result);                                   //Postman de body - row ardından sağ taraftan json ı seçtik.
        }
    
    
    
    }
}
