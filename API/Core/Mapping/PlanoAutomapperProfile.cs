using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class PlanoAutomapperProfile : Profile
    {
        public PlanoAutomapperProfile()
        {            
            CreateMap<Plano, PlanoViewModel>().ReverseMap()
                .ForMember(dest => dest.PlanoId, opt => opt.MapFrom(src => src.PlanoId))
                .ForMember(dest => dest.NomePlano, opt => opt.MapFrom(src => src.NomePlano));

            CreateMap<Plano, CriarPlanoInputModel>().ReverseMap()
                .ForMember(dest => dest.NomePlano, opt => opt.MapFrom(src => src.NomePlano));

            CreateMap<PlanoViewModel, CriarPlanoInputModel>().ReverseMap()
                .ForMember(dest => dest.NomePlano, opt => opt.MapFrom(src => src.NomePlano));

            CreateMap<Plano, PlanoInputModel>().ReverseMap()
                .ForMember(dest => dest.NomePlano, opt => opt.MapFrom(src => src.NomePlano));
        }
    }
}
