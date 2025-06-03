using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseSubGrupoAutomapperProfile : Profile
    {
        public AnamneseSubGrupoAutomapperProfile()
        {
            CreateMap<AnamneseSubGrupo, AnamneseSubGrupoViewModel>()
                .ForMember(dest => dest.AnamneseSubGrupoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoId));
        }
    }
}
