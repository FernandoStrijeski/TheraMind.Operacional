using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ProfissionalAutomapperProfile : Profile
    {
        public ProfissionalAutomapperProfile()
        {            
            CreateMap<Profissional, ProfissionalViewModel>()
                .ForMember(dest => dest.ProfissionalId, opt => opt.MapFrom(src => src.ProfissionalId))
                .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.NomeCompleto));
        }
    }
}
