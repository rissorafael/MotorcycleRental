using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleRental.Test
{
    public class EntregadorServiceTest
    {
        private readonly Mock<IFormFile> _mockFormFile;

        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IEntregadorRepository> _mockEntregadorRepository;
        private readonly Mock<ILogger<EntregadorService>> _mockLogger;
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly Mock<IRolesService> _mockRolesService;


        private readonly EntregadorService _entregadorService;
   
        public EntregadorServiceTest()
        {
            _mockFormFile = new Mock<IFormFile>();
            _mockMapper = new Mock<IMapper>();
            _mockEntregadorRepository = new Mock<IEntregadorRepository>();
            _mockLogger = new Mock<ILogger<EntregadorService>>();
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mockRolesService = new Mock<IRolesService>();


            _entregadorService = new EntregadorService(
                           _mockMapper.Object,
                           _mockEntregadorRepository.Object,
                           _mockLogger.Object,
                           _mockUsuarioService.Object,
                           _mockRolesService.Object);


        }
    }
}