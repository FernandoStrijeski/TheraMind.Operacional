using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class AgendaSessaoAutomapperProfile : Profile
    {
        public AgendaSessaoAutomapperProfile()
        {
            CreateMap<AgendaSessao, AgendaSessaoViewModel>().ReverseMap()
                .ForMember(dest => dest.AgendaSessaoId, opt => opt.MapFrom(src => src.AgendaSessaoId));

            CreateMap<AgendaSessao, CriarAgendaSessaoInputModel>().ReverseMap();

            CreateMap<AgendaSessaoViewModel, CriarAgendaSessaoInputModel>().ReverseMap();

            CreateMap<AgendaSessao, AgendaSessaoInputModel>().ReverseMap();
        }
    }
}
