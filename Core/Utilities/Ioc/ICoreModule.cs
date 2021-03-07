using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Ioc
{
    public interface ICoreModule   //Her hangi bir teknolojiden bağımsız bir İnteface dir.Başka bir teknolojiye geçildiğinde yine kullanılabilir.
    {
        void Load(IServiceCollection serviceCollection);    //Genel bağımlılıkları yükleyecek metodun imzasıdır.
    }
}
