using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseSubGrupoQuestaoAutomapperProfile : Profile
    {
        public AnamneseSubGrupoQuestaoAutomapperProfile()
        {
            CreateMap<AnamneseSubGrupoQuestao, AnamneseSubGrupoQuestaoViewModel>()
                .ForMember(dest => dest.AnamneseSubGrupoQuestaoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoQuestaoId));
        }
    }
}
