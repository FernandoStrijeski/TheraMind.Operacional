using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class CidadeAutomapperProfile : Profile
    {
        public CidadeAutomapperProfile()
        {            
            CreateMap<Cidade, CidadeViewModel>().ReverseMap()
                .ForMember(dest => dest.CidadeId, opt => opt.MapFrom(src => src.CidadeId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Cidade, CriarCidadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<CidadeViewModel, CriarCidadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Cidade, CidadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
