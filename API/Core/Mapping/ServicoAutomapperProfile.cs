using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ServicoAutomapperProfile : Profile
    {
        public ServicoAutomapperProfile()
        {            
            CreateMap<Servico, ServicoViewModel>()
                .ForMember(dest => dest.ServicoId, opt => opt.MapFrom(src => src.ServicoId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
