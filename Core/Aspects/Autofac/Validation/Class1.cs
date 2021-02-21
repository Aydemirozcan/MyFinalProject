using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)              //Attribute lara tipleri böyle atarız.
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))                //Eğer kullanıcının verdiği validatorType bir IValidator degilse ona bu uyarıyı ver.
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);                        //Activator.CreateInstance(_validatorType) burada Reflexion'dır  (_validatorType ın bir İnstance sını oluşturur.).(Burada _validatorType örneğin bizim ProductValidator)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];                           //ProductValidatorun çalışma tipini bul. //ProductValidatorun BaseType 'ı gidip bakıldığında ( AbstractValidator<Product> ) ve bunun generic argümanlarında ilk ini bul. 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);                  //Ve onun parametrelerini bul  // ( invocation ) metot demek //Validatorun tipine eşit olan parametreleri bul.(ProductValidator un ki product dır) entityType bu sebepten var.//Çünkü Bir metotda birden fazla parametre olabilir,Birden fazla Validational da olabilir.
            foreach (var entity in entities)                                                            //Herbir parametre yi tek tek gez (çünkü birden fazla parametre olabilir) 
            {
                ValidationTool.Validate(validator, entity);                                            //ValidationTool u kullanarak Validate et
            }
        }
    }
}
