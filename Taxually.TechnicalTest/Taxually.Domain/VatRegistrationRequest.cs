using System.ComponentModel.DataAnnotations;

namespace Taxually.Domain
{
    public class VatRegistrationRequest
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyId { get; set; } =  string.Empty;
        public string Country { get; set; } = string.Empty;
    }
}
