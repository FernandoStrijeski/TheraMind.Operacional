using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class PlanoAutomapperProfile : Profile
    {
        public PlanoAutomapperProfile()
        {            
            CreateMap<Plano, PlanoViewModel>()
                .ForMember(dest => dest.PlanoId, opt => opt.MapFrom(src => src.PlanoId))
                .ForMember(dest => dest.NomePlano, opt => opt.MapFrom(src => src.NomePlano));
        }
    }
}
