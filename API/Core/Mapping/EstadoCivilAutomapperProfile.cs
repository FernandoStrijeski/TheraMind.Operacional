using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EstadoCivilAutomapperProfile : Profile
    {
        public EstadoCivilAutomapperProfile()
        {            
            CreateMap<EstadoCivil, EstadoCivilViewModel>().ReverseMap()
                .ForMember(dest => dest.EstadoCivilId, opt => opt.MapFrom(src => src.EstadoCivilId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EstadoCivil, CriarEstadoCivilInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EstadoCivilViewModel, CriarEstadoCivilInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EstadoCivil, EstadoCivilInputModel>().ReverseMap()
                .ForMember(dest => dest.EstadoCivilId, opt => opt.MapFrom(src => src.EstadoCivilId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
