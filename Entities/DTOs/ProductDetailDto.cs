using Core;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class ProductDetailDto:IDto    //Tek başına bir tablo değil,Belki birkaç tablonun birkaç kolonuna karşılık geliyor ,Bu yüzden IEntity demedik.Gidip context yapmasın diye.
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
    }
}
