using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class SalaAutomapperProfile : Profile
    {
        public SalaAutomapperProfile()
        {            
            CreateMap<Sala, SalaViewModel>().ReverseMap()
                .ForMember(dest => dest.SalaId, opt => opt.MapFrom(src => src.SalaId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Sala, CriarSalaInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<SalaViewModel, CriarSalaInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Sala, SalaInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
