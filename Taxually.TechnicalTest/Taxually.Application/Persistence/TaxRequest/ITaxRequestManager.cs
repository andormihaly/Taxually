
using Taxually.Domain;

namespace Taxually.Application.Persistence.TaxRequest
{
    public interface ITaxRequestManager
    {
        Task ResolveRequest(VatRegistrationRequest request);
    }
}
