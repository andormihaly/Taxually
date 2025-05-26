using System.Xml.Serialization;
using Taxually.Domain;
using Taxually.Persistence.Clients;

namespace Taxually.Persistence.Resolvers
{
    internal class QueueXmlResolver : IRegistrationResolver
    {
        public async Task Manage(VatRegistrationRequest request)
        {
            using (var stringwriter = new StringWriter())
            {
                var serializer = new XmlSerializer(typeof(VatRegistrationRequest));
                serializer.Serialize(stringwriter, request);
                var xml = stringwriter.ToString();
                var xmlQueueClient = new TaxuallyQueueClient();
                // Queue xml doc to be processed
                await xmlQueueClient.EnqueueAsync("vat-registration-xml", xml);
            }
        }
    }
}
