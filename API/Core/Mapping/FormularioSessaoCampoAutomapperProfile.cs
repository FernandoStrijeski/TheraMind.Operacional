using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class FormularioSessaoCampoAutomapperProfile : Profile
    {
        public FormularioSessaoCampoAutomapperProfile()
        {
            CreateMap<FormularioSessaoCampo, FormularioSessaoCampoViewModel>()
                .ForMember(dest => dest.FormularioSessaoCampoId, opt => opt.MapFrom(src => src.FormularioSessaoCampoId));
        }
    }
}
