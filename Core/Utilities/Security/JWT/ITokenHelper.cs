using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);  //CreateToken token üreten mekanizmadır.Peki hangi token ları versin List<OperationClaim> listesinden gelecek değerler.
    }
}
                                                                                   //Müşteri kullanıcı adı ve parolayı girdikten sonra  bu Api ye gider orada Api bu kullanıcının OperationClaimlerine bakar ve karşılığında Müşteriye bir JWT verir.
