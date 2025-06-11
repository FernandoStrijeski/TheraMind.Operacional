using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ClienteAutomapperProfile : Profile
    {
        public ClienteAutomapperProfile()
        {
            CreateMap<Cliente, ClienteViewModel>().ReverseMap()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId));            

            CreateMap<Cliente, CriarClienteInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.NomeCompleto));

            CreateMap<ClienteViewModel, CriarClienteInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.NomeCompleto));

            CreateMap<Cliente, ClienteInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.NomeCompleto));
        }
    }
}
