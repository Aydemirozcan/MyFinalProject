using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework         //Bu class bir Entityframework kullanarak bir repository base i oluştur demektir.
{                                                 //(<TEntity,TContext>)Bu yapı bana bir tane tablo ver ve çalışacağım Context tipini ver demek oluyor.
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>      //(IEntityRepository deki operasyonlarıda kullanabilmek için yaptık)Hangi tabloyu verirsem onun Entity Repository si olucak anlamı taşıyor
        where TEntity : class ,IEntity ,new()
        where TContext: DbContext ,new()
    {                                                                         //EfProdctdal dakileri kesip buraya yapıştırdık.Product olan yerlere : Tentity , NorthwindContext olanlara TContext yazdık. 
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of c#   (Using'in anlamı)
            using (TContext context = new TContext())                             //Us yazınca çıktı tab tab yaptık using içerisine yazdığımız şeyler using bitince silinir(G.Collector tarafından)--- Normal şekilde de newleyerek yapabiliriz ama:
            {                                                                     //using içerisine yazınca daha performanslı oluyor.
                var addedEntity = context.Entry(entity);                          //1.Satır :git veri kaynağından benim bu gönderğimdim product a bir nesneyi eşleştir.Ama bu ekleme işlemi olduğu için herhangi bir şeyle eşleştirmeyecek direk ekleyecek.
                addedEntity.State = EntityState.Added;                            //Entry Entity Framework e ait birşey
                context.SaveChanges();                                            //1.satır: referansı yakala ,2.satır: o aslında eklenecek bir nesne 3.satır: ekle
            }
        }

        public void Delete(TEntity   entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)                 //Tek birşey getirir.
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)               //Filtre verebilir ama istemezsede vermez çünkü default 'u null.
        {
            using (TContext context = new TContext())                             //Eğer ki filtre göndermemişse veri tabanındaki tüm datayı getir.Eğer ki filtre vermişse onu uygula.İlk kısımda filtre yoksa ikinci kısmı uygular. 
            {
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();                                //context.Set<Product>().ToList()   Aslında arka tarafta select(*) from products 'ı çalıştırır ve bir liste halinde verir.
            }
        }

        public List<TEntity> GetAllByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
