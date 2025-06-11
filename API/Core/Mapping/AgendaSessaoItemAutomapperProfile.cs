using API.modelos.InputModels;
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
            CreateMap<AgendaSessaoItem, AgendaSessaoItemViewModel>().ReverseMap()
                .ForMember(dest => dest.AgendaSessaoItemId, opt => opt.MapFrom(src => src.AgendaSessaoItemId));

            CreateMap<AgendaSessaoItem, CriarAgendaSessaoItemInputModel>().ReverseMap();

            CreateMap<AgendaSessaoItemViewModel, CriarAgendaSessaoItemInputModel>().ReverseMap();

            CreateMap<AgendaSessaoItem, AgendaSessaoItemInputModel>().ReverseMap();
        }
    }
}
