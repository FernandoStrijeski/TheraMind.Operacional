using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class FormularioSessaoAutomapperProfile : Profile
    {
        public FormularioSessaoAutomapperProfile()
        {
            CreateMap<FormularioSessao, FormularioSessaoViewModel>().ReverseMap()
                .ForMember(dest => dest.FormularioSessaoId, opt => opt.MapFrom(src => src.FormularioSessaoId));

            CreateMap<FormularioSessao, CriarFormularioSessaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<FormularioSessaoViewModel, CriarFormularioSessaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<FormularioSessao, FormularioSessaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
