using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{                                                                    //Validation u heryerde kullanabilmek için bir tool haline getirdik.
    public static class ValidationTool                           //Bu tip tool lar genellikle static yapılır.Tek bir intance oluşturur ve uygulama belleği onu kullanır.Bir daha new lemeyelim diye.
    {                                                          //C# da static bir classın metodlarınında static olması gerekir.Java da böyle değil.
                                                                           //Oluşturduğumuz ProductValidator a inherit ettiğimiz AbstractValidator F12 ile içerisine gidildiğinde içinde bir IValidator bulundurduğunu gördük.O da Validate i bulunduruyordu. 
        public static void Validate(IValidator validator,object entity)              //IValidator ün Ampulünden Install package 'FluentValidation' Use local version 9.5.1 i seçtik.   //Buradaki validator bir Ivalidator dür örneğin: ProductValidator ve entity de bir örnek product dır.Biri doğrulama kurallarının olduğu diğeri doğrulanacak classtır.
        {                                                                            //herşey gelebilsin diye (Entity ,Dto ...) object yazdık.(satır11)
                                                                                  // Bir Validation yapılacağı zaman aşağıdaki kodlar standart dır.
             var context = new ValidationContext<object>(entity);              //Burdaki context ilgili bir tiredi anlatır.(Aynı Entity Framework gibi)//Bu satırda entity için doğrulama yapıcağımızı belirttik.        

            var result = validator.Validate(context);                          //verilen validator deki kuralları kullanarak ilgili contexti Validate et.
            if (!result.IsValid)
            {                                                               //Sonuç geçerli değilse hata fırlat.//Valid değilse hata ver.
                throw new ValidationException(result.Errors);
            }

        }
    }

}
