using Taxually.Domain;

namespace Taxually.Persistence.Resolvers
{
    public interface IRegistrationResolver
    {
        Task Manage(VatRegistrationRequest request);
    }
}
