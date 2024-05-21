using AutoMapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Mappings
{
    public class DomainMappingToProfileDto : Profile
    {
        public DomainMappingToProfileDto()
        {
            CreateMap<MotoRequestModel, Moto>();
            CreateMap<EntregadorRequestModel, Entregador>();
            CreateMap<ContratoRequestModel, Contrato>();
            CreateMap<LocacaoRequestModel, Locacao>();
            CreateMap<UsuarioRequestModel, Usuario>();
            CreateMap<RolesRequestModel, Roles>();
        }
    }
}
