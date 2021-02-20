using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Çıplak Class Kalmasın.(Eğer ki bir class bir inheritage veya interface implementasyonu almıyorsa bilki ilerde bir sorun ile karşılacaksın.)
    public class Category:IEntity    //Bunu yaptıkdan sonra ampülden using Entites.Abstract seçtik. Ve yukarıya kendiliğinden yazıldı.
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
