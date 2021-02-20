using Core.Entities;
using System;                           
using System.Collections.Generic;                           //Core katmanı diğer katmanlara bağlanmaz----Yıldızlı Not
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess                     //Core katmanı başka hiçbir katmanı referans almaz.
{
     //generic constraint                                           //Generic Repository Design Pattern  (Bu yapının adı)  
     //class: where T: 'den sonra class yazmak , referans tip olabilir demektir(int filan getirme diye)
     //IEntity: IEntity olabilir veya IEntity implemete eden bir nesne olabilir.
     //new() : new'lenebilir olmalı yani IEntity 'i de eledik.(İnterface newlenemez) IEntity nin imkanlarından faydalandık ama onu almadık.
    public interface IEntityRepository<T> where T:class,IEntity,new()       //Ya IEntity olabilir yada ondan referans olan bişey olabilir.
    {
        List<T> GetAll(Expression<Func<T,bool>> filter =null);                                    //Parantez içindeki yapının nedeni tek tek category ıd ye göre getir veya name ine göre getir şeklinde ayrı ayrı metotlar yazmadan bu şekilde yazabiliriz.
                                                                                                  //filter =null demek filtre vermeyebilirsin demektir.Filtre vermiyosa tüm data yı istiyor demektir.Veriyorsa filtreliyip getiriyor demektir.
        T Get(Expression<Func<T, bool>> filter);                                                  //Linq deki parantez içindeki kısım gibi bize filtreleme imkanı sundu.
        void Add(T entity);
        void Update(T entity);

        void Delete(T entity);


       // List<Product> GetAllByCategory(int categoryId);  //Yukarıdaki gibi expression ile yapınca buna hiç ihtiyacımız kalmadı.

    }
}
