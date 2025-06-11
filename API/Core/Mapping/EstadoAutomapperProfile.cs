using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EstadoAutomapperProfile : Profile
    {
        public EstadoAutomapperProfile()
        {            
            CreateMap<Estado, EstadoViewModel>().ReverseMap()
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Uf))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Estado, CriarEstadoInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EstadoViewModel, CriarEstadoInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Estado, EstadoInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
