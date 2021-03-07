using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)        //Burda yapılan şey şu:Çalıştırdığınız metdoun namespacesi class ismi metod ismi ve parametrelerine göre bir key oluştur.eğer daha önce bu key varsa onu çalıştır.yoksa git veri tabanından veriyi çek metodu çalıştır ve cache yede ekle.
        {
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}"); //Namespace Class ismi ve method ismi
            var arguments = invocation.Arguments.ToList();                                                 //parametereleri bulur
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";   //string join demek bir araya getir aralarına virgül koy parametrelerin her biri için //2 soru işareti varsa bunu ekle yoksa bunu ekle (null değilse ve stringe çevirlebiliyorsa).//arguments.Select parametre değerlerini listeye döndürür.

            if (_cacheManager.IsAdd(key))   //varsa cache de onu çalıştır.
            {
                invocation.ReturnValue = _cacheManager.Get(key);   // invocation.ReturnValue   metodun return değeri //normal de return gidip veritabanından alıyordu ama bu sefer veritabanına gitme varolan değeri döndür demek = returnValue
                return;
            }
            invocation.Proceed();  //yoksa cache de git veritabanından çek cacaheyede ekle 
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
