using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxually.Application.Exceptions;
using Taxually.Application.Features.VatRegistration.Commands;
using Taxually.Application.MappingProfiles;
using Taxually.Application.Persistence.TaxRequest;
using Taxually.Application.UnitTests.Mocks;
using Taxually.TechnicalTest.Controllers;

namespace Taxually.Application.UnitTests.Features.VatRegistration.Commands
{
    public class TaxManagerTests
    {
        private readonly Mock<ITaxRequestManager> _mockManager;

        private IMapper _mapper;

        private Mock<IMediator> _mediator;

        private CreateVatRegistrationRequestCommandHandler _handler;
        public TaxManagerTests()
        {
            _mockManager = MockTaxRequestManager.GetRequestManagerMock();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<VatRegistrationProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateVatRegistrationRequestCommandHandler(_mapper, _mockManager.Object);

            _mediator = new Mock<IMediator>();
            _mediator.Setup(m => m.Send(It.IsAny<CreateVatRegistrationRequestCommand>(), It.IsAny<CancellationToken>()))
                        .Returns<CreateVatRegistrationRequestCommand, CancellationToken>((cmd, token) => _handler.Handle(cmd, token));

        }
        [Fact]
        public async void Post_Should_Call_ResolveRequest_When_Request_Is_Valid()
        {
            var controller = new VatRegistrationController(_mediator.Object);

            var request = new CreateVatRegistrationRequestCommand
            {
                CompanyId = "123",
                CompanyName = "Test Ltd",
                Country = "DE"
            };

            // Act
            var result = await controller.Post(request);

            // Assert
            Assert.IsType<OkResult>(result);

            _mockManager.Verify(m => m.ResolveRequest(It.IsAny<Domain.VatRegistrationRequest>()), Times.Once);
        }
        [Fact]
        public async void Post_Should_Call_ResolveRequest_When_Request_Is_Not_Valid()
        {
            var controller = new VatRegistrationController(_mediator.Object);

            var invalidRequest = new CreateVatRegistrationRequestCommand
            {
                CompanyId = "123",
                CompanyName = "Alfa",
                Country = "HU" //not supported
            };

            // Act + Assert
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => controller.Post(invalidRequest));

            Assert.Equal("Invalid Leave Request", exception.Message);

            // Verify ResolveRequest was never called
            _mockManager.Verify(m => m.ResolveRequest(It.IsAny<Domain.VatRegistrationRequest>()), Times.Never);
        }
    }
}