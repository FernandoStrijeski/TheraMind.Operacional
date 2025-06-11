using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseSubGrupoAutomapperProfile : Profile
    {
        public AnamneseSubGrupoAutomapperProfile()
        {
            CreateMap<AnamneseSubGrupo, AnamneseSubGrupoViewModel>().ReverseMap()
                .ForMember(dest => dest.AnamneseSubGrupoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoId));

            CreateMap<AnamneseSubGrupo, CriarAnamneseSubGrupoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<AnamneseSubGrupoViewModel, CriarAnamneseSubGrupoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<AnamneseSubGrupo, AnamneseSubGrupoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
