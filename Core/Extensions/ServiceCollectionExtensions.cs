using Core.Utilities.Ioc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection , ICoreModule[] modules)   //Burada this ve virgüle kadar olan kısım parametre değil sadece neyin genişletileceği belirtiliyor.parametre virgülden sonrası için geçerli
        {
            foreach (var module in modules)          //modules deki her bir module için module yükle
            {
                module.Load(serviceCollection);
            }
            return ServiceTool.Create(serviceCollection);
        }
    }
}                                                         //IServiceCollection: Bizim Asp.Net uygulamamaızın (Apimizin) service bağımlılıklarını eklediğimiz,yada araya girmesini istediğimiz koleksiyondur.
