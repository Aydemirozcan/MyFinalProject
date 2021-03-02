using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Business.Constants;
using Core.Extensions;

namespace Business.BusinessAspects.Autofac    //Authorasiyion Aspect leri genellikle Bussines a yazılır çünkü her projenin yetkilendirme algortiması değişebilir.Zaten altyapımızı Core a yazdık sadece Aspect kısmınıo proje ile ilgili Aspect kısmını Business ın içine yazıyoruz.
                                              //Her metodun başında şunun için yetkisi var bunun için yetkisi var mı? diye soracağımıza bir Aspect yazarız metodun başında onu kullanırız.Bizim yerimize yetkilendirmeyi yapar.
{
    public class SecuredOperation : MethodInterception              //Bu yazdığımız SecuredOperation JWT için yazıldı.
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;   //IHttpContextAccessor:  JWT da birlikte göndererek bir istek yapıldığında her istek için her bir kişi için HttpContext i oluşur.Herkese bir tane tired oluşur.
                                                             //Bu bizim karşımıza bir Interface olarak gelir.Ampulden Install package Microsoft.AspNetCore.Http i seçtik.
        public SecuredOperation(string roles)   //Bu ctor da bana rolleri ver dedik.
        {
            _roles = roles.Split(',');             //Split: Bir metni senin belirttiğin karaktere göre ayırıp Arrye atıyor.
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();       //IHttpContextAccessor : Asp.Net core un dan gelen bir yapıdır.Aynı Configuration gibi enjekte etmeniz gerekiyor.

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();  //O anki kullanıcının ClaimRol lerini bul.
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))              //Bu kullanıcının rollerini gez eğer Claimlerinin içinde ilgili rol varsa return et(metodu çalıştırmaya devam et)(hata verme)
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);    //Yoksa yetkisi bu hatayı ver.
        }
    }
}
