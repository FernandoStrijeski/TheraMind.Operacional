using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class TipoEtniaAutomapperProfile : Profile
    {
        public TipoEtniaAutomapperProfile()
        {            
            CreateMap<TipoEtnia, TipoEtniaViewModel>().ReverseMap()
                .ForMember(dest => dest.TipoEtniaId, opt => opt.MapFrom(src => src.TipoEtniaId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<TipoEtnia, CriarTipoEtniaInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<TipoEtniaViewModel, CriarTipoEtniaInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<TipoEtnia, TipoEtniaInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
