using AutoMapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Mappings
{
    public class DtoMappingToProfileDomain : Profile
    {
        public DtoMappingToProfileDomain()
        {
            CreateMap<Moto, MotoResponseModel>();
            CreateMap<Locacao, LocacaoResponseModel>();
            CreateMap<Entregador, EntregadorResponseModel>();
            CreateMap<Planos, PlanosResponseModel>();
            CreateMap<Contrato, ContratoResponseModel>();
            CreateMap<Usuario, UsuarioResponseModel>();
            CreateMap<Roles, RolesResponseModel>();
            CreateMap<TransacaoMensagem, LocacaoRequestModel>();
        }
    }
}
