using API.Operacional.DTO.Empresa;
using API.Operacional.modelos.ViewModels;
using API.modelos.InputModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class TipoLogradouroAutomapperProfile : Profile
    {
        public TipoLogradouroAutomapperProfile()
        {            
            CreateMap<TipoLogradouro, TipoLogradouroViewModel>().ReverseMap()
                .ForMember(dest => dest.TipoLogradouroId, opt => opt.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<TipoLogradouro, CriarTipoLogradouroInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<TipoLogradouroViewModel, CriarTipoLogradouroInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<TipoLogradouro, TipoLogradouroInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
