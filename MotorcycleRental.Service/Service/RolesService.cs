using AutoMapper;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;


namespace MotorcycleRental.Service.Service
{
    public class RolesService : IRolesService
    {
        private readonly IMapper _mapper;
        private readonly IRolesRepository _rolesRepository;
        private readonly ILogger<RolesService> _logger;

        public RolesService(IMapper mapper, IRolesRepository rolesRepository, ILogger<RolesService> logger)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<List<RolesResponseModel>> GetByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                var roles = await _rolesRepository.GetByUsuarioIdAsync(usuarioId);
                var response = _mapper.Map<List<RolesResponseModel>>(roles);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[RolesService - GetByUsuarioIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<RolesResponseModel> AddAsync(RolesRequestModel request)
        {
            try
            {
                var response = new RolesResponseModel();

                var rolesRequest = _mapper.Map<Roles>(request);
                var rolesResponse = await _rolesRepository.AddAsync(rolesRequest);

                response = _mapper.Map<RolesResponseModel>(rolesResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[RolesService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }
    }
}
