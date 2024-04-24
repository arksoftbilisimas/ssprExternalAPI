namespace SSPRExternalApiExample.Api.Constants
{
    public class ExternalApiConstants
    {
        //Web portaldan alinacak api key
        public const string ExternalApiKey = "6c1facd3-3733-3850-4b3d-679bc2721d9a";

        //Apinin bulundugu host adresi
        public const string ExternalApiHost = "https://localhost:44371";



        ////////////Degistirlmeyecek alan////////////
        //Api key header'dan beklenmektedir. Bu yuzden bu alanin degistirilmemesi gerekir.
        public const string AuthorizationHeaderKey = "auth";
        //External apide kullanilacak endpoint adresleri. Bu alanin degistirilmemesi gerekir.
        public const string GetDomainPrefix = "/ExternalApi/GetAllDomain";
        public const string ResetPasswordWithLinkPrefix = "/ExternalApi/ResetPasswordWithLink";
        public const string ResetPasswordWithRandomPasswordPrefix = "/ExternalApi/ResetPasswordWithRandomPassword";
        ////////////Degistirlmeyecek alan////////////
    }
}
