using Taxually.Persistence.Resolvers;

namespace Taxually.Persistence.Builders
{
    public interface IResolverBuilder
    {
        IRegistrationResolver Build(string countryCode);
    }
}
