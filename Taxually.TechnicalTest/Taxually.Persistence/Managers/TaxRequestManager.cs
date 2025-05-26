using Taxually.Application.Persistence.TaxRequest;
using Taxually.Domain;
using Taxually.Persistence.Builders;

namespace Taxually.Persistence.Managers
{
    public class TaxRequestManager : ITaxRequestManager
    {
        private readonly IResolverBuilder _resolverBuilder;

        public TaxRequestManager(IResolverBuilder resolverBuilder)
        {
            _resolverBuilder = resolverBuilder;
        }
        public async Task ResolveRequest(VatRegistrationRequest request)
        {
             await _resolverBuilder.Build(request.Country).Manage(request);
        }
    }
}
