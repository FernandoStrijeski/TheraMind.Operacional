using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ConvenioAutomapperProfile : Profile
    {
        public ConvenioAutomapperProfile()
        {
            CreateMap<Convenio, ConvenioViewModel>()
                .ForMember(dest => dest.ConvenioId, opt => opt.MapFrom(src => src.ConvenioId));
        }
    }
}
