using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class PacoteFechadoAutomapperProfile : Profile
    {
        public PacoteFechadoAutomapperProfile()
        {
            CreateMap<PacoteFechado, PacoteFechadoViewModel>()
                .ForMember(dest => dest.PacoteFechadoId, opt => opt.MapFrom(src => src.PacoteFechadoId));
        }
    }
}
