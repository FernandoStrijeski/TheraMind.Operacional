using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class TipoDocumentoAutomapperProfile : Profile
    {
        public TipoDocumentoAutomapperProfile()
        {            
            CreateMap<TipoDocumento, TipoDocumentoViewModel>()
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
