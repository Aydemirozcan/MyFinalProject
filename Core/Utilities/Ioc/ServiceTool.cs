using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Ioc
{
    public static class ServiceTool           //ServiceTool un amacı şimdiye kadar yazdığımız Service leri Windows-Form içerisinde de kulllanabilmektir.Örneğin: ServiceTool.ServiceProvider.GetService<IProductService>()  diyerek bu Service i çekebiliriz.
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)   //.Net in Service lerini al ve onları build et
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;                                                   //Bağımlılık webapi business dataccess diye gittiği için aspect bu zincirin içinde kalmıyor bizde Coredaki instanceleri çekebilmek için  Servicetool yazdık.
        }                                                                      //Bunu da ICoreModule a enjekte ettik.
    }                                                                          //Bu zincirin içinde olmayan intanceleri bu şekilde getiriyoruz.
}
