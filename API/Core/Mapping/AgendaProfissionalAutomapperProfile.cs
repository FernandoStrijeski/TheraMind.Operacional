using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AgendaProfissionalAutomapperProfile : Profile
    {
        public AgendaProfissionalAutomapperProfile()
        {
            CreateMap<AgendaProfissional, AgendaProfissionalViewModel>()
                .ForMember(dest => dest.AgendaProfissionalId, opt => opt.MapFrom(src => src.AgendaProfissionalId));
        }
    }
}
