namespace SSPRExternalApiExample.Api.Dtos
{
    //Ornek olarak Active Directory kullanilmistir. Bu dto kullanilmasi sart degildir.
    public class DomainDto
    {
        public Guid ObjectGuid { get; set; }
        public string? FQDN { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public int Port { get; set; }

        public bool TLS { get; set; }

    }
}
