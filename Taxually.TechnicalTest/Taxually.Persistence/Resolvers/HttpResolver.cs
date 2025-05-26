using Taxually.Domain;
using Taxually.Persistence.Clients;

namespace Taxually.Persistence.Resolvers
{
    public class HttpResolver : IRegistrationResolver
    {
        public async Task Manage(VatRegistrationRequest request)
        {
            var httpClient = new TaxuallyHttpClient();

            await httpClient.PostAsync("https://api.uktax.gov.uk", request);
        }
    }
}
