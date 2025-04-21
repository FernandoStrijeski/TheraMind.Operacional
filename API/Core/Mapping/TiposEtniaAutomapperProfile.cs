using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class TiposEtniaAutomapperProfile : Profile
    {
        public TiposEtniaAutomapperProfile()
        {            
            CreateMap<TipoEtnia, TipoEtniaViewModel>()
                .ForMember(dest => dest.TipoEtniaId, opt => opt.MapFrom(src => src.TipoEtniaId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
