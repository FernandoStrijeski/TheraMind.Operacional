using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AgendaProfissionalAutomapperProfile : Profile
    {
        public AgendaProfissionalAutomapperProfile()
        {            
            CreateMap<AgendaProfissional, AgendaProfissionalViewModel>().ReverseMap()
                .ForMember(dest => dest.AgendaProfissionalId, opt => opt.MapFrom(src => src.AgendaProfissionalId));

            CreateMap<AgendaProfissional, CriarAgendaProfissionalInputModel>().ReverseMap();

            CreateMap<AgendaProfissionalViewModel, CriarAgendaProfissionalInputModel>().ReverseMap();

            CreateMap<AgendaProfissional, AgendaProfissionalInputModel>().ReverseMap();
        }
    }
}
