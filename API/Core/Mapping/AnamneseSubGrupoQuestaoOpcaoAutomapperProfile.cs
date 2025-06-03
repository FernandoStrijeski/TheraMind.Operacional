using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseSubGrupoQuestaoOpcaoAutomapperProfile : Profile
    {
        public AnamneseSubGrupoQuestaoOpcaoAutomapperProfile()
        {
            CreateMap<AnamneseSubGrupoQuestaoOpcao, AnamneseSubGrupoQuestaoOpcaoViewModel>()
                .ForMember(dest => dest.AnamneseSubGrupoQuestaoOpcaoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoQuestaoOpcaoId));
        }
    }
}
