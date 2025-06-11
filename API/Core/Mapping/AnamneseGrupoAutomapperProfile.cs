using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseGrupoAutomapperProfile : Profile
    {
        public AnamneseGrupoAutomapperProfile()
        {
            CreateMap<AnamneseGrupo, AnamneseGrupoViewModel>().ReverseMap()
                .ForMember(dest => dest.AnamneseGrupoId, opt => opt.MapFrom(src => src.AnamneseGrupoId));    

            CreateMap<AnamneseGrupo, CriarAnamneseGrupoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<AnamneseGrupoViewModel, CriarAnamneseGrupoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<AnamneseGrupo, AnamneseGrupoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
