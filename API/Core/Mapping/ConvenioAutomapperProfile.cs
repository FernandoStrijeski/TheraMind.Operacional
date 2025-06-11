using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ConvenioAutomapperProfile : Profile
    {
        public ConvenioAutomapperProfile()
        {
            CreateMap<Convenio, ConvenioViewModel>().ReverseMap()
                .ForMember(dest => dest.ConvenioId, opt => opt.MapFrom(src => src.ConvenioId));

            CreateMap<Convenio, CriarConvenioInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ConvenioViewModel, CriarConvenioInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Convenio, ConvenioInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
