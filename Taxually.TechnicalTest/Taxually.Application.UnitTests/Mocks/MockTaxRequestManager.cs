using Moq;
using Taxually.Application.Persistence.TaxRequest;

namespace Taxually.Application.UnitTests.Mocks
{
    internal class MockTaxRequestManager
    {
        public static Mock<ITaxRequestManager> GetRequestManagerMock()
        {
            var mockManager = new Mock<ITaxRequestManager>();
            mockManager.Setup(m => m.ResolveRequest(It.IsAny<Domain.VatRegistrationRequest>()))
                       .Returns(Task.CompletedTask);

            return mockManager;
        }
    }
}
