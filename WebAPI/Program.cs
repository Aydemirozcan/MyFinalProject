using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
                                                                                   //CreateHostBuilder burasý yayýn konusunda konfigürasyonun olduðu yerdir.
        public static IHostBuilder CreateHostBuilder(string[] args) =>             //Burasý server la ilgili konfigürasyonun yapýldýðý yer.//.Net Core içerisinde ki IoC altyapýsýný kullanmýyoruz fabrika olarak Autofac i kullanýyoruz.
            Host.CreateDefaultBuilder(args)                                        //Bu satýrdan aþaðýsý .Net in WebAPI sine kendi altyapsýný kullanmamasýný ,fabrika olrak Autofac i kullanmasýný söylediðimiz yer.
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())       //Servis saðlayýcý fabrikasý olarak kullan //AutofacServiceProviderFactory() yazdýktan sonra ampul den Install package 'Autofac.Extensions.DependencyInjection' ve sonra Find and Intall latest version u seçtik.
                .ConfigureContainer<ContainerBuilder>(builder =>                 //Autofac kullanacaðýmýzý belirttik ama Autofac için AutofacBusinessModule diye birþey yazdýk onuda getirip buraya yazdýk.
                {
                    builder.RegisterModule(new AutofacBusinessModule());          //Eðer ki bir gün Autofac den de vazgeçer yerine baþka birþey kullanýrsak DependencyResolvers ýn içine yeni sistemin dosyasýný oluþturup ,
                })                                                                //Buraya da gelip AutofacServiceProviderFactory() ve AutofacBusinessModule() ýn yerine de yeni sistemi konfigüre etmemiz gerekir. 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
