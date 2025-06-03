using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class FormularioSessaoAutomapperProfile : Profile
    {
        public FormularioSessaoAutomapperProfile()
        {
            CreateMap<FormularioSessao, FormularioSessaoViewModel>()
                .ForMember(dest => dest.FormularioSessaoId, opt => opt.MapFrom(src => src.FormularioSessaoId));
        }
    }
}
