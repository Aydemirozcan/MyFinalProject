using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module                             //WebAPI deki StartUp da yazdığımız ortamı Autofac de kuruyoruz.Yani .Net in IoC yapılandırması var ama biz kendi IoC yapılandırmamızı yapıyoruz.
    {                                                                     //Autofac e geçmemizin sebebi bize aynı zamanda AOP desteği veriyor. 
        protected override void Load(ContainerBuilder builder)                                            //Overr yazıp space ye bastık.(Virtual metotları bu şekilde ezeriz.)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();             //Birisi senden IProductService isterse ona bir tane ProductManager Instance 'ı ver.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();                   //SingleInstance tek bir Instance oluşturuyor herkese onu veriyor.Çünkü bunlar referans tip bellekteki tek bir referans numarasını herkese veriyor.İçerisinde data tutmaz sadece operasyon çağırmaya yarar.Data taşır ama tutmaz.
        }
    }
}
