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
                                                                                   //CreateHostBuilder buras� yay�n konusunda konfig�rasyonun oldu�u yerdir.
        public static IHostBuilder CreateHostBuilder(string[] args) =>             //Buras� server la ilgili konfig�rasyonun yap�ld��� yer.//.Net Core i�erisinde ki IoC altyap�s�n� kullanm�yoruz fabrika olarak Autofac i kullan�yoruz.
            Host.CreateDefaultBuilder(args)                                        //Bu sat�rdan a�a��s� .Net in WebAPI sine kendi altyaps�n� kullanmamas�n� ,fabrika olrak Autofac i kullanmas�n� s�yledi�imiz yer.
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())       //Servis sa�lay�c� fabrikas� olarak kullan //AutofacServiceProviderFactory() yazd�ktan sonra ampul den Install package 'Autofac.Extensions.DependencyInjection' ve sonra Find and Intall latest version u se�tik.
                .ConfigureContainer<ContainerBuilder>(builder =>                 //Autofac kullanaca��m�z� belirttik ama Autofac i�in AutofacBusinessModule diye bir�ey yazd�k onuda getirip buraya yazd�k.
                {
                    builder.RegisterModule(new AutofacBusinessModule());          //E�er ki bir g�n Autofac den de vazge�er yerine ba�ka bir�ey kullan�rsak DependencyResolvers �n i�ine yeni sistemin dosyas�n� olu�turup ,
                })                                                                //Buraya da gelip AutofacServiceProviderFactory() ve AutofacBusinessModule() �n yerine de yeni sistemi konfig�re etmemiz gerekir. 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
