using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseGrupoAutomapperProfile : Profile
    {
        public AnamneseGrupoAutomapperProfile()
        {
            CreateMap<AnamneseGrupo, AnamneseGrupoViewModel>()
                .ForMember(dest => dest.AnamneseGrupoId, opt => opt.MapFrom(src => src.AnamneseGrupoId));
        }
    }
}
