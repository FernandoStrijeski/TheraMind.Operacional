using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class NacionalidadeAutomapperProfile : Profile
    {
        public NacionalidadeAutomapperProfile()
        {            
            CreateMap<Nacionalidade, NacionalidadeViewModel>()
                .ForMember(dest => dest.NacionalidadeID, opt => opt.MapFrom(src => src.NacionalidadeId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
