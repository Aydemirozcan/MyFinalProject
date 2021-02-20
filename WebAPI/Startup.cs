using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup                  //Ýleride .Net in kendi IoC Container ýna Autofac enjekte ediyor olacaðýz.
    {                                     //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject -->IoC Container
                                          //Postsharp (ücretli)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)          //33 ve 34 üncü satýrlarý biz yazdýk.Orada baþka þeyler vardý onlarý sildik.
        {
            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>();     //Controller der ki:Eðer ki sen IProductService tipinde bir baðýmlýlýk görürsen  onun karþýlýðý ProductManager dýr.
            //services.AddSingleton<IProductDal, EfProductDal>();          //Yani Singleton arka tarafta bizim yerimize newler ve sadece bir kere newler her yerde de onu kullanýr.Bu da çok performanslý olur.
        }                                                                //Singleton yapabilmemiz için somut içerisinde data tutmamamýz gerekir.Zaten buradaki ProductManager içerisinde data yok sadece metotlar var. 
                                                                         //Yani sonuç olarak Biri bizden ctor da IProductService isterse bu da (services.AddSingleton<IProductService,ProductManager>(); ) bize ProductManageri newleyip verir.
       // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
