using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //context:Db tabloları ile proje classlarını bağlamak   //NorthwindContext'i Db tabloları ile bizim oluşturduğumuz class ları bağlamak için oluşturduk.
    public class NorthwindContext:DbContext                                      //DbContext Entity Framework kurunca içinde böyle bir sınıf geldi.
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)                                             //Bu metot senin projen hangi veri tabanı ile ilişkiliyi belirttiğin yer.
        {                                                                                                                         //override on   yazınca aşağıda çıktı ve kod bu hale geldi.(Süslü parantez içindeki kodu sildik ve SQL Server kullanacağımız için ona nasıl bağlanacağımızı belirttik.(optionsBuilder.UseSqlServer())
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_connection=true");              //Başına @ koyduğumuzda   (\)ters slaşı , normal slaş(/) olarak algıla demektir.
        }                                                                                                                         //SQL Server ımızın ismi normalde bir IP numarası olur ama biz şuan development ortamında olduğumuz için ; 
                                                                                                                                  //Üstteki View mesünden SQL Server Object Explorer ' a tıklayıp , SQL Server kısmında (localdb)\mssqllocaldB yazdık (Kopyala-Yapıştırda yapabilirdik) ve sonra bu server da hangi veritabanı olduğunu bildirdik
        public DbSet<Product> Products { get; set; }                                                                              //;Database=Nortwind yazdık veri tabanını belirtmiş olduk,Sonrasında  ;Trusted_connection=true yazarak Kullanıcı adı ve şifre kullanmayacağımızı belirttik.Sonuç olarak Entity framework un nereye bağlanacağını vermiş oduk. 
        public DbSet<Category> Categories { get; set; }                                                                           //Ek bilgi : Trusted_connection=true ile instrested security true aynı şeydir.   
        public DbSet<Customer> Customers { get; set; }                                     //hangi nesneyi hangisiyle bağlayacağımızı belirttik(Dbset nesnesini kullanarak) (Hangi tablo hangi class a denk gelecek onu belirttik) 
        public DbSet<Order> Orders { get; set; }

        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
