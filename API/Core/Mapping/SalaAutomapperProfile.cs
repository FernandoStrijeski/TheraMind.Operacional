using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class SalaAutomapperProfile : Profile
    {
        public SalaAutomapperProfile()
        {            
            CreateMap<Sala, SalaViewModel>()
                .ForMember(dest => dest.SalaId, opt => opt.MapFrom(src => src.SalaId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
