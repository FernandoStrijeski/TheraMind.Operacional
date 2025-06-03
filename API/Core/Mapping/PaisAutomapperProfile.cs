using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class PaisAutomapperProfile : Profile
    {
        public PaisAutomapperProfile()
        {            
            CreateMap<Pais, PaisViewModel>()
                .ForMember(dest => dest.PaisID, opt => opt.MapFrom(src => src.PaisId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Estado, EstadoViewModel>()
                .ForMember(dest => dest.Uf, opt => opt.MapFrom(src =>src.Uf))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src =>src.Descricao));

            CreateMap<Cidade, CidadeViewModel>()
                .ForMember(dest => dest.CidadeId, opt => opt.MapFrom(src => src.CidadeId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.CodigoIbge, opt => opt.MapFrom(src => src.CodigoIbge));

        }
    }
}
