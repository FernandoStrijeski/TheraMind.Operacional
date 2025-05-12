using API.AdmissaoDigital.modelos.ViewModels;
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
        }
    }
}
