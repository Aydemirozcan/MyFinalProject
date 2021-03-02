using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper    //Bu bir araç olduğu için çıplak kalabilir.
    {
        public static void CreatePasswordHash(string password ,out byte[] passwordHash ,out byte[] passwordSalt)             //Hem password ün hashini oluşturur hemde saltını oluşturur.//Buradaki out u dışarıya verilecek değer gibide düşünebiliriz.//Out key wordünü birden fazla veriyi döndürmek için kullanmayı simule ettik. 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())                                   //hmac kritografi sınıfında kullandığımız class a karşılık geliyor.
            {
                passwordSalt = hmac.Key;                                                                  //Salt olarak hmac deki Key değerini vereceğiz.//Buradaki Key ilgili kullandığımız algoritmanın o an için oluşturduğu Key değeridir.Her kullanıcı için ayrı Key değeri oluşturur.Çok da güvenlidir.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));                    //Bir string in byte değerini almak istiyorsanız şu yöntemi kullanabilirsiniz.(Encoding.UTF8.GetBytes(password)   password burada string dir biz onun byte değerini almak istiyoruz. 
            }
        }                                                                                            //Verdiğiniz bir password değerine göre salt ve hash değerini oluşturmaya çalışıyor.
        
        public static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)      //Kullanıcının girdiği şifreyi yine aynı algoritmayı(yukarıdakini) kullanarak aynı hash değerine ulaşırmıydın ulaşamazmıydın onu doğrular.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))              
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)                                      //Hesaplanan Arrayin bütün değerlerini tek tek dolaş.
                {
                    if (computedHash[i]!=password[i])                                             //Eğerki benim computedHashim   i nci değere eşit değilse (veitabanından gönderilen hash ile) 
                    {
                        return false;
                    }
                }
                return true;                                                                     //Daha önce hash leme yaparken kullandığımız tuzlama ile onu doğruladık.
            }            
        }
    
    }
}
