using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxually.Application.Features.VatRegistration.Commands;
using Taxually.Application.MappingProfiles;
using Taxually.Domain;
using Taxually.Persistence.Builders;
using Taxually.Persistence.Managers;
using Taxually.Persistence.Resolvers;
using Taxually.TechnicalTest.Controllers;

namespace Taxually.Application.Tests.Integration
{
    public class PersistenceTests
    {

        private IMapper _mapper;

        private Mock<IMediator> _mediator;

        private CreateVatRegistrationRequestCommandHandler _handler;

        private Mock<IRegistrationResolver> _mockResolver;

        private Mock<IResolverBuilder> _mockBuilder;
        public PersistenceTests()
        {

            _mockResolver = new Mock<IRegistrationResolver>();
            _mockResolver.Setup(r => r.Manage(It.IsAny<VatRegistrationRequest>()))
                        .Returns(Task.CompletedTask);


            _mockBuilder = new Mock<IResolverBuilder>();
            _mockBuilder.Setup(b => b.Build("DE"))
                        .Returns(_mockResolver.Object);

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<VatRegistrationProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            var taxRequestManager = new TaxRequestManager(_mockBuilder.Object);

            _handler = new CreateVatRegistrationRequestCommandHandler(_mapper, taxRequestManager);

            _mediator = new Mock<IMediator>();
            _mediator.Setup(m => m.Send(It.IsAny<CreateVatRegistrationRequestCommand>(), It.IsAny<CancellationToken>()))
                        .Returns<CreateVatRegistrationRequestCommand, CancellationToken>((cmd, token) => _handler.Handle(cmd, token));
        }

        [Fact]
        public async Task Post_Should_Call_Manage_Of_Expected_Resolver_When_Country_Is_DE()
        {
            // Arrange
            
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
            var okResult = Assert.IsType<OkResult>(result);
            _mockResolver.Verify(r => r.Manage(It.IsAny<VatRegistrationRequest>()), Times.Once);
            _mockBuilder.Verify(b => b.Build("DE"), Times.Once);
        }

        }
    
}
