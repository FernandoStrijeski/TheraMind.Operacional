using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EstadoCivilAutomapperProfile : Profile
    {
        public EstadoCivilAutomapperProfile()
        {            
            CreateMap<EstadoCivil, EstadoCivilViewModel>()
                .ForMember(dest => dest.EstadoCivilId, opt => opt.MapFrom(src => src.EstadoCivilId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
