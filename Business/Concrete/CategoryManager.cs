using Business.Abstract;
using Core.Utilities.Results;
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

        public IDataResult<List<Category>> GetAll()
        {
            //İş Kodları
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }
        //Select * from Categories where CategoryId=3
        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
