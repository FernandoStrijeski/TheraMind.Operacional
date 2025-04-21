using API.AdmissaoDigital.DTO.Empresa;
using API.AdmissaoDigital.modelos.ViewModels;
using API.modelos.InputModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class TiposLogradouroAutomapperProfile : Profile
    {
        public TiposLogradouroAutomapperProfile()
        {            
            CreateMap<TipoLogradouro, TipoLogradouroViewModel>()
                .ForMember(dest => dest.TipoLogradouroId, opt => opt.MapFrom(src => src.TipoLogradouroId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
