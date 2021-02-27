using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
                                                                         //WebAPI deki StartUp da yazdığımız ortamı Autofac de kuruyoruz.Yani .Net in IoC yapılandırması var ama biz kendi IoC yapılandırmamızı yapıyoruz.                      
namespace Business.DependencyResolvers.Autofac                           //Autofac e geçmemizin sebebi bize aynı zamanda AOP desteği veriyor.
{
    public class AutofacBusinessModule:Module                             //Module yazdıktan sonra ampulden using Autofac i seçtik.          
    {
        protected override void Load(ContainerBuilder builder)                                            //Overr yazıp space ye bastık.(Virtual metotları bu şekilde ezeriz.)Sonra çıkanlar arasında load ı seçtik.
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();             //Birisi senden IProductService isterse ona bir tane ProductManager Instance 'ı ver.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();                   //SingleInstance tek bir Instance oluşturuyor herkese onu veriyor.Çünkü bunlar referans tip bellekteki tek bir referans numarasını herkese veriyor.İçerisinde data tutmaz sadece operasyon çağırmaya yarar.Data taşır ama tutmaz.
                                                                                                       //Bu yaptığımız işlemler WebAPI daki Startup da yapılanların Autofac karşılığıdır.Orda yazılanla aynı anlamdadır.

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();


            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}

                                                                                                 //Birden fazla WebAPI veya farklı servis yapıları kullanırsak diye Autofac i biz Business katmanına kurduk.
                                                                                                //Hatta biz onu bir Console uygulamasının içinde veya bir Windows Form uygulamasının içinde de kullanmak istersek Business katmanında olması bize ciddi faydalar sağlar.
