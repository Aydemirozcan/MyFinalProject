using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{                                                                    //Validation u heryerde kullanabilmek için bir tool haline getirdik.
    public static class ValidationTool                           //Bu tip tool lar genellikle static yapılır.Tek bir intance oluşturur ve uygulama belleği onu kullanır.Bir daha new lemeyelim diye.
    {                                                          //C# da static bir classın metodlarınında static olması gerekir.Java da böyle değil.
                                                                           //Oluşturduğumuz ProductValidator a inherit ettiğimiz AbstractValidator F12 ile içerisine gidildiğinde içinde bir IValidator bulundurduğunu gördük.O da Validate i bulunduruyordu. 
        public static void Validate(IValidator validator,object entity)              //IValidator ün Ampulünden Install package 'FluentValidation' Use local version 9.5.1 i seçtik.
        {                                                                            //herşey gelebilsin diye (Entity ,Dto ...) object yazdık.(satır11)
            var context = new ValidationContext<Object>(entity);                     
            
            var result = validator.Validate(context);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

        }
    }

}
