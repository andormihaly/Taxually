using System.Text;
using Taxually.Domain;
using Taxually.Persistence.Clients;

namespace Taxually.Persistence.Resolvers
{
    internal class QueueCsvResolver : IRegistrationResolver
    {
        public async Task Manage(VatRegistrationRequest request)
        {
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("CompanyName,CompanyId");
            csvBuilder.AppendLine($"{request.CompanyName}{request.CompanyId}");
            var csv = Encoding.UTF8.GetBytes(csvBuilder.ToString());
            var excelQueueClient = new TaxuallyQueueClient();
            // Queue file to be processed
            await excelQueueClient.EnqueueAsync("vat-registration-csv", csv);
        }
    }
}
