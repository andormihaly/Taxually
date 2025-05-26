using Taxually.Persistence.Resolvers;

namespace Taxually.Persistence.Builders
{
    internal interface IResolverBuilder
    {
        IRegistrationResolver Build(string countryCode);
    }
}
