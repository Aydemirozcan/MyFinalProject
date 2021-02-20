using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{                                       
    public class Product: IEntity  //public bu class a diğer katmanlarda ulaşabilsin demektir.//Bir class ın default 'u internal dır. internal demek sadece Entities erişebilir demektir.(yani içersinde bulunduğu katman olarak)
    {                              //Aynı şekilde buna da ampulden Using Entities.Abstract dedik.
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }     //short int in bir küçüğüdür.Bizim veri tabanımızda small int olarak verildiği için bu şekilde yaptık.
        public decimal UnitPrice { get; set; }
    }
}
