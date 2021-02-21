using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{                                                                    //İşlem yapılması istenen nesnenin yapısı ile ilgili olan şeyler doğrulamalar validation dur.
    public class ProductValidator : AbstractValidator<Product>   
    {                                                                    //Doğrulama kodlarını buraya yazıyoruz.
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);                                        //Örneğin İçecek kategorisindekiler ürün fiyatı min. 10 lira olmalı.
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");                              //StartWithA metodu aslında VS da yok onu biz yazdık.Ampulden Generate Method u seçtik.
                                                                                                                                    //Yanına mesaj yazmamızın sebebi diğerlerinde mesajı FluentValidation verebiliyor( 19 dile desteği var)ama bunu biz yazdığımız için mesajını da yazdık.//Tarayıcınız İngilizce ise hata mesajları ingilizce de gelebilir.
        }

        private bool StartWithA(string arg)                    //A ile başlıyosa True döner başlamıyorsa False döner.
        {
            return arg.StartsWith("A");
        }
    }
}
