using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class GrauParentescoAutomapperProfile : Profile
    {
        public GrauParentescoAutomapperProfile()
        {            
            CreateMap<GrauParentesco, GrauParentescoViewModel>()
                .ForMember(dest => dest.GrauParentescoID, opt => opt.MapFrom(src => src.GrauParentescoId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
