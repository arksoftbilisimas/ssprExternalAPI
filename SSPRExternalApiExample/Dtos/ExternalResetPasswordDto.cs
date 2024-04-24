namespace SSPRExternalApiExample.Api.Dtos
{
    public class ExternalResetPasswordDto
    {
        public string SamAccountName { get; set; }
        public Guid DomainObjectGUID { get; set; }
    }
}
