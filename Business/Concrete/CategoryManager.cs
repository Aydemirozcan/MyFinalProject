﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;              //İleride teknolojiyi değiştirirsek diye burada constructor injection yaptık.Bu şekilde bağımlılık oluşturduk
                                                //Şu demek oluyor "Ben categoryManager olarak veri erişim katmanına bağlıyım ama biraz zayıf bağımlılığım var.
                                                //Çünkü İnterface üzerinden referans üzerinden bağımlılığım var.DataAccess de istediğin kadar dolaş ama benim burdaki kurallarıma uy"
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            //İş Kodları
            return _categoryDal.GetAll();
        }
        //Select * from Categories where CategoryId=3
        public Category GetById(int categoryId)
        {
            return _categoryDal.Get(c => c.CategoryId == categoryId);
        }
    }
}
