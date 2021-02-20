using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet: başkalarınında kodlarını kullandığımız biryer
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal        //EfProdctdal dakileri kesip EfEntityRepositoryBase e yapıştırmıştık,IProductDal bize bunun için tekrardan implemente etmemiz gerektiğini söylüyordu
                                                                                                      //ama biz buraya (IProductDal)ın yanına EfEntityRepositoryBase<Product,NorthwindContext> yazınca zaten IProducDal ın imzası olan operasyonlar inherit edilmiş oldu.
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context =new NorthwindContext())
            {
                var result = from p in context.Products                                                                 //Burası ürünlerle kategorileri join et demek
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId                                                        //Neye göre join edeceğimizi yazdık.Eğer bu kolonlar eşitse onları join et
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId, ProductName = p.ProductName ,                                           //Hangi kolonları istiyorsun?Sonucu şu kolonlara uydurarak ver.
                                 CategoryName = c.CategoryName, UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }
    }
}
