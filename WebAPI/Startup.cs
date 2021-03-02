using Business.Abstract;
using Business.Concrete;
using Core.Utilities.Ioc;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup                  //�leride .Net in kendi IoC Container �na Autofac enjekte ediyor olaca��z.
    {                                     //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject -->IoC Container
                                          //Postsharp (�cretli)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)          //33 ve 34 �nc� sat�rlar� biz yazd�k.Orada ba�ka �eyler vard� onlar� sildik.
        {
            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>();     //Controller der ki:E�er ki sen IProductService tipinde bir ba��ml�l�k g�r�rsen  onun kar��l��� ProductManager d�r.
            //services.AddSingleton<IProductDal, EfProductDal>();          //Yani Singleton arka tarafta bizim yerimize newler ve sadece bir kere newler her yerde de onu kullan�r.Bu da �ok performansl� olur.
                                                                           //Singleton yapabilmemiz i�in somut i�erisinde data tutmamam�z gerekir.Zaten buradaki ProductManager i�erisinde data yok sadece metotlar var. 
                                                                           //Yani sonu� olarak Biri bizden ctor da IProductService isterse bu da (services.AddSingleton<IProductService,ProductManager>(); ) bize ProductManageri newleyip verir.


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();   //Asp.net webAPI ' a bu sistemde autotantikasyon olarak JWTBearerToken kullan�lacak diye belirtti�imiz yerdir.

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters=new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            ServiceTool.Create(services);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();      //Buna Middleware deniyor.Asp.Net ya�am d�ng�s�nde hangi yap�lar�n s�ras�yla devreye girece�ini s�yl�yorsunuz. 

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
