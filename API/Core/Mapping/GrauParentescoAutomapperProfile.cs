using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class GrauParentescoAutomapperProfile : Profile
    {
        public GrauParentescoAutomapperProfile()
        {                       
            CreateMap<GrauParentesco, GrauParentescoViewModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<GrauParentesco, CriarGrauParentescoInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<GrauParentescoViewModel, CriarGrauParentescoInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<GrauParentesco, GrauParentescoInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
