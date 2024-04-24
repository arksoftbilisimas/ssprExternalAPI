using Microsoft.AspNetCore.Mvc;
using SSPRExternalApiExample.Api.Dtos;
using SSPRExternalApiExample.Api.Helpers;

namespace SSPRExternalApiExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost("ResetPasswordWithLink")]
        public async Task<ActionResult> ResetPasswordWithLink(string sicilNo)
        {
            ///////////////////////////////////////////////
            //Ornek olarak Active Directory kullanilmistir.
            //Bunun yerine kendinize ait olan herhangi bir dogrulama yontemiyle, parolası sifirlanacak samaccount name'e ulasabilirsiniz.

            DomainDto domain = new()
            {
                FQDN = "domain.com",
                UserName = "username",
                Password = "password",
                Port = 389,
                TLS = false
            };

            var resGetSamAccountName = await ADHelper.Instance.GetSamAccountName(sicilNo, domain);
            if (!resGetSamAccountName.isOk)
            {
                return BadRequest("User Not Found");
            }

            ///////////////////////////////////////////////


            //Eğer external apinin istedigi object guid'sini bilmiyorsaniz, SSPR projesinde eklediğiniz domain bilgileriniz cekmek icin "GetAllDomains" metodunu kullanabilirsiniz. Gelen listeye gore dto'yu olusturabilirsiniz.
            //
            //var resultAllDomains = await ApiHelper.Instance.GetAllDomains(new CancellationTokenSource());
            //

            ExternalResetPasswordDto externalResetPassword = new ExternalResetPasswordDto()
            {
                DomainObjectGUID = new Guid("27e8ece5-9e9a-4625-921d-53c772594e8c"), 
                SamAccountName = resGetSamAccountName.samAccountName
            };

            //Olusturulan dto da ki secili DomainObjectGUID ve SamAccountName ile parola sifirlamak isteyen kullanicinin active directory'de kayitli telefon numarasına sifre resetleme linki gonderilir.

            var result = await ApiHelper.Instance.ResetPasswordWithLink(externalResetPassword, new CancellationTokenSource());

            return result.IsOk ? Ok(result.Data) : BadRequest(result.Data);

        }

        [HttpPost("ResetPasswordWithRandomPassword")]
        public async Task<ActionResult> ResetPasswordWithRandomPassword(string sicilNo)
        {
            ///////////////////////////////////////////////
            //Ornek olarak Active Directory kullanilmistir.
            //Bunun yerine kendinize ait olan herhangi bir dogrulama yontemiyle, parolası sifirlanacak samaccount name'e ulasabilirsiniz.

            DomainDto domain = new()
            {
                FQDN = "domain.com",
                UserName = "username",
                Password = "password",
                Port = 389,
                TLS = false
            };

            var resGetSamAccountName = await ADHelper.Instance.GetSamAccountName(sicilNo, domain);
            if (!resGetSamAccountName.isOk)
            {
                return BadRequest("User Not Found");
            }

            ///////////////////////////////////////////////

            //Eğer external apinin istedigi object guid'sini bilmiyorsaniz, SSPR projesinde eklediğiniz domain bilgileriniz cekmek icin "GetAllDomains" metodunu kullanabilirsiniz. Gelen listeye gore dto'yu olusturabilirsiniz.
            //
            //var resultAllDomains = await ApiHelper.Instance.GetAllDomains(new CancellationTokenSource());
            //

            ExternalResetPasswordDto externalResetPassword = new ExternalResetPasswordDto()
            {
                DomainObjectGUID = new Guid("27e8ece5-9e9a-4625-921d-53c772594e8c"),
                SamAccountName = resGetSamAccountName.samAccountName
            };

            //Olusturulan dto da ki secili DomainObjectGUID ve SamAccountName ile parola sifirlamak isteyen kullanicinin active directory'de kayitli telefon numarasına rastgele sifre sifre uretilerek gonderilir.
            var result = await ApiHelper.Instance.ResetPasswordWithRandomPassword(externalResetPassword, new CancellationTokenSource());
            return result.IsOk ? Ok(result.Data) : BadRequest(result.Data);

        }
    }
}
