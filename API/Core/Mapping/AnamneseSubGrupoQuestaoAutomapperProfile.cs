using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseSubGrupoQuestaoAutomapperProfile : Profile
    {
        public AnamneseSubGrupoQuestaoAutomapperProfile()
        {
            CreateMap<AnamneseSubGrupoQuestao, AnamneseSubGrupoQuestaoViewModel>().ReverseMap()
                .ForMember(dest => dest.AnamneseSubGrupoQuestaoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoQuestaoId));

            CreateMap<AnamneseSubGrupoQuestao, CriarAnamneseSubGrupoQuestaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<AnamneseSubGrupoQuestaoViewModel, CriarAnamneseSubGrupoQuestaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<AnamneseSubGrupoQuestao, AnamneseSubGrupoQuestaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
