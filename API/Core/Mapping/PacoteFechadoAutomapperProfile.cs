using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class PacoteFechadoAutomapperProfile : Profile
    {
        public PacoteFechadoAutomapperProfile()
        {
            CreateMap<PacoteFechado, PacoteFechadoViewModel>()
                .ForMember(dest => dest.PacoteFechadoId, opt => opt.MapFrom(src => src.PacoteFechadoId));


            CreateMap<PacoteFechado, CriarPacoteFechadoInputModel>().ReverseMap()
                .ForMember(dest => dest.QuantidadeSessoes, opt => opt.MapFrom(src => src.QuantidadeSessoes));

            CreateMap<PacoteFechadoViewModel, CriarPacoteFechadoInputModel>().ReverseMap()
                .ForMember(dest => dest.QuantidadeSessoes, opt => opt.MapFrom(src => src.QuantidadeSessoes));

            CreateMap<PacoteFechado, PacoteFechadoInputModel>().ReverseMap()
                .ForMember(dest => dest.QuantidadeSessoes, opt => opt.MapFrom(src => src.QuantidadeSessoes));
        }
    }
}
