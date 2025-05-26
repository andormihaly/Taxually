using System;
using Taxually.Persistence.Resolvers;

namespace Taxually.Persistence.Builders
{
    public class ResolverBuilder : IResolverBuilder
    {
        public IRegistrationResolver Build(string countryCode)
        {
            switch (countryCode)
            {
                case "GB":
                    return new HttpResolver();
                case "FR":
                    return new QueueCsvResolver();
                case "DE":
                    return new QueueXmlResolver();
                default:
                    throw new Exception("Country not supported");

            }
        }
    }
}
