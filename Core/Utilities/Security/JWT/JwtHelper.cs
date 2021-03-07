using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }     //Apideki appsettings i okumana yarıyor.
        private TokenOptions _tokenOptions;              //Appsetting den okuduğumuz değerleri bu nesneye atacağız.
        private DateTime _accessTokenExpiration;          //AccessToken ne zaman geçersizleşecek onu veriyor.
        public JwtHelper(IConfiguration configuration)    //.Net Corun verdiği bu konfigürasyon nesnesini enjekte ettik.
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();    //Appsettings içindeki bölümü al hangi bölümü TokenOptions ı Ve onuTokenOptions un değerlerini kullanarak map le.

        }
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)   //Bu kullanıcı için bir tane token üretiyor olacağız.
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);      //Şimdi ye dakika ekledik.(Appsettings de 10 dk. dediğimiz için şimdiye 10 dk eklemiş olduk) Bu kullanıcının 10 dakika izni vardı.
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);           //Bunu oluştururken bir securityKey e ihtiyacımız olacak.Yazdığımız SecurityKeyHelper ın CreateSecurityKey ine tokenOptions daki SecurityKey i kullanarak onu oluşturabilirsin.
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);    //Hangi algoritmayı kullanayım ve anahtar nedir? ---Bunun içinde SigningCredentialHelper diye birşey yazdık onun içinde onlar var 
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);   // CreateJwtSecurityToken : tokeOptions u kullanarak ilgili kullanıcı için ilgili Credentials kullanarak  bu kullanıcıya atanacak yetkileri içeren bir metotdur.
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();                              //Modeltokenjwt den geliyor
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,       //CreateJwtSecurityToken  (Yukarıda açıkladığımız metodu oluşturduk)  //JwtSecurityToken -- Microsoft.IdentityModel.Tokens dan geldi.
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(            //JwtSecurityToken token oluşturmaya yarıyor.Ve aşağıda gerekli bilgiler oluşturuldu.
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,                      //Token ın Expiration bilgisi şuandan önce ise geçerli değil.
                claims: SetClaims(user, operationClaims),       //Claim ler içinde aşağıda bir metod yapılmış.
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)   //Claimler yetkiden daha fazlasıdır.
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");            //2 tane string i yanyana getirmek istersek çift tırnak içerisinde başına da dolar işareti koyarsak bu biççimde 2 stringi yanyana gösterebiliriz. 
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());   //Roller ekliyoruz.operationClaims deki kullanıcının ismini seçip Array haline getirip rollere ekliyoruz.

            return claims;
        }
    }
}
                                                    //.Net de bize ait olmayan bir nesneye yeni metodlar eklenebilir buna Extension (Genişletmek) denir.Var olan bir class a kendi metodlarını ekleyebilirsin.C# bir Extension metod yazabilmen için hem class ın hem metodun static olması gerekir.
                                                    //Yukarıdaki metoda AddNameIdentifier AddEmail AddName  AddRoles ekleyebilmek için  ClaimExtensions classı nı oluşturduk. 