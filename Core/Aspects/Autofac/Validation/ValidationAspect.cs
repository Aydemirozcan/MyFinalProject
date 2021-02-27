using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{                                                                 //Bana bir validatorType ver (   typeof(ProductValidator)   )   Attribute lere type böyle verilir.   
    public class ValidationAspect : MethodInterception  //Aspect :metodun başında sonu ve hata verdiğinde çalışacak olan şeydir.
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)              //Attribute lara tipleri böyle atarız.  //Aslında aşağıdaki if i hiç yazmasakta kod çalışır ama biz sadece istediğimiz type ları alabilsin diye bunu yaptık  (defensive coding)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))              //Göndermeye çalıştığın validatorType IsAssignable yani atanabiliyor mu?  Yani IValidator mü?
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil");            //Eğer kullanıcının verdiği validatorType bir IValidator degilse ona bu uyarıyı ver.
            }

            _validatorType = validatorType;                                              //validatorType olduğuna emin olduktan sonra eşitleniyor.
        }
        protected override void OnBefore(IInvocation invocation)                                        //Madem ben bir Methodİnterception ım benim ezmemi istediğin bir metod var mı sorusunun cevabı olarak burayı yazıyoruz.
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);                        //Activator.CreateInstance(_validatorType) burada Reflexion'dır  (_validatorType ın bir İnstance sını oluşturur.).(Burada _validatorType örneğin bizim ProductValidator)//Yani Product Validator u newledi.
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];                           //ProductValidatorun çalışma tipini bul. //ProductValidatorun BaseType 'ı gidip bakıldığında ( AbstractValidator<Product> ) ve bunun generic argümanlarında ilk ini bul. 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);                  //Ve onun parametrelerini bul  // ( invocation ) metot demek //Validatorun tipine eşit olan parametreleri bul.(ProductValidator un ki product dır) entityType bu sebepten var.//Çünkü Bir metotda birden fazla parametre olabilir,Birden fazla Validational da olabilir.
            foreach (var entity in entities)                                                            //Herbir parametre yi tek tek gez (çünkü birden fazla parametre olabilir) 
            {
                ValidationTool.Validate(validator, entity);                                            //ValidationTool u kullanarak Validate et
            }
        }                                                                                             //Bu yazdığımız son kısım validationun sadece metodun başında çalışacağını anlatır.Doğrulama metodun başında çalışır.
    }
}
