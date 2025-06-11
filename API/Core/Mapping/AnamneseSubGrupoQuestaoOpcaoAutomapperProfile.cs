using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseSubGrupoQuestaoOpcaoAutomapperProfile : Profile
    {
        public AnamneseSubGrupoQuestaoOpcaoAutomapperProfile()
        {
            CreateMap<AnamneseSubGrupoQuestaoOpcao, AnamneseSubGrupoQuestaoOpcaoViewModel>().ReverseMap()
                .ForMember(dest => dest.AnamneseSubGrupoQuestaoOpcaoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoQuestaoOpcaoId));

            CreateMap<AnamneseSubGrupoQuestaoOpcao, CriarAnamneseSubGrupoQuestaoOpcaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto));

            CreateMap<AnamneseSubGrupoQuestaoOpcaoViewModel, CriarAnamneseSubGrupoQuestaoOpcaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto));

            CreateMap<AnamneseSubGrupoQuestaoOpcao, AnamneseSubGrupoQuestaoOpcaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto));
        }
    }
}
