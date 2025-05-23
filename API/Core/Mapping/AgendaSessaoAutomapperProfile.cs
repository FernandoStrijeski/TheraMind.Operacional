using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class AgendaSessaoAutomapperProfile : Profile
    {
        public AgendaSessaoAutomapperProfile()
        {
            CreateMap<AgendaSessao, AgendaSessaoViewModel>()
                .ForMember(dest => dest.AgendaSessaoId, opt => opt.MapFrom(src => src.AgendaSessaoId));
        }
    }
}
