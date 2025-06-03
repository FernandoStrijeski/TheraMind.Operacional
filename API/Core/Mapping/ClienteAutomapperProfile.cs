using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ClienteAutomapperProfile : Profile
    {
        public ClienteAutomapperProfile()
        {
            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));
        }
    }
}
