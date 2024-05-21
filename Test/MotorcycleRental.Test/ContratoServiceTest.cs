using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using MotorcycleRental.Service.Service;


namespace MotorcycleRental.Test
{
    public class ContratoServiceTest
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IContratoRepository> _mockContratoRepository;
        private readonly Mock<ILogger<ContratoService>> _mockLogger;
        private readonly Mock<ILocacaoService> _mockLocacaoService;
        private readonly Mock<IPlanosService> _mockPlanosService;
        private readonly Mock<IProducer> _mockProducer;

        private readonly ContratoService _contratoService;

        public ContratoServiceTest()
        {

            _mockMapper = new Mock<IMapper>();
            _mockContratoRepository = new Mock<IContratoRepository>();
            _mockLogger = new Mock<ILogger<ContratoService>>();
            _mockLocacaoService = new Mock<ILocacaoService>();
            _mockPlanosService = new Mock<IPlanosService>();
            _mockProducer = new Mock<IProducer>();


            _contratoService = new ContratoService(
                _mockMapper.Object,
                _mockContratoRepository.Object,
                _mockLogger.Object,
                _mockLocacaoService.Object,
                _mockPlanosService.Object,
                _mockProducer.Object
                            );
        }

        [Fact]
        public async Task GetByIdLocacaoAsync_ReturnsContratoResponseModel()
        {
            // Arrange
            var idLocacao = 1;
            var contrato = new Contrato { Id = idLocacao };
            var contratoResponseModel = new ContratoResponseModel { Id = idLocacao };

            _mockContratoRepository.Setup(repo => repo.GetByIdLocacaoAsync(idLocacao))
                .ReturnsAsync(contrato);

            _mockMapper.Setup(mapper => mapper.Map<ContratoResponseModel>(contrato))
                .Returns(contratoResponseModel);

            // Act
            var result = await _contratoService.GetByIdLocacaoAsync(idLocacao);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(idLocacao, result.Id);
            _mockContratoRepository.Verify(repo => repo.GetByIdLocacaoAsync(idLocacao), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ContratoResponseModel>(contrato), Times.Once);
        }

        [Fact]
        public async Task AddAsync_ShouldReturnErrorResponse_WhenContratoAlreadyExists()
        {
            // Arrange
            int idLocacao = 1;
            DateTime dataFim = DateTime.Now.AddDays(10);

            _mockContratoRepository
                .Setup(x => x.GetByIdLocacaoAsync(idLocacao))
                .ReturnsAsync(new Contrato()); // Simula que o contrato já existe

            // Act
            var result = await _contratoService.AddAsync(idLocacao, dataFim);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("Não existe locacao com Id", result.Description);
        }
    }
}