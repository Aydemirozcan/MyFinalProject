using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Ioc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Core.DependencyResolvers             //Genel bağlılıklarımızı tüm projelerde kullanacağımız injectionları DependencyResolvers klasörüne a koyacağız.
{                                              
    public class CoreModule : ICoreModule     //uygulama seviyesinde servis bağımlılıklarımızı çözümleyeceğimiz yer olacak.
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();                                 //AddMemoryCache .Netin kendisinindir//Bunu yazınca IMemoryCache instancesi oluşuyor arka planda //Bunu yazdığımız için artık kendisi otomatik injection yapıyor.(  IMemoryCache _memoryCache; yapıyor)   //Bu satırı yazdığımız zaman artık _memoryCache ' in karşılığı var. Ama ICacheManageri enjekte etmek istersekde aşağı satırlardaki MemoryCacheManager var.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();     //HttpContextAccessor her yapılan istekle ilgili olan context dir.Client ımız istek yaptığı zaman başlagıcından bitişine kadar.Reqiest den response ye kadar olan süreçte takip edilme işlemini yapar.
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();         //Birisi senden IcacheMager isterse ona Microsoftun MemoryCacheManager ını ver//İleride redise geçmek istersen MemoryCacheManager silip RedisCacheManager yazman yeterli. (Yukarıdaki AddMemoryCache ' ide iptal etmen gerekiyor.)
            serviceCollection.AddSingleton<Stopwatch>();

        }
    }
}
