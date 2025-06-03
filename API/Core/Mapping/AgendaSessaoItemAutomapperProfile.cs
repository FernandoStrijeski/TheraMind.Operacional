using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class AgendaSessaoItemAutomapperProfile : Profile
    {
        public AgendaSessaoItemAutomapperProfile()
        {
            CreateMap<AgendaSessaoItem, AgendaSessaoItemViewModel>()
                .ForMember(dest => dest.AgendaSessaoItemId, opt => opt.MapFrom(src => src.AgendaSessaoItemId));
        }
    }
}
