using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class FormularioSessaoCampoAutomapperProfile : Profile
    {
        public FormularioSessaoCampoAutomapperProfile()
        {
            CreateMap<FormularioSessaoCampo, FormularioSessaoCampoViewModel>().ReverseMap()
                .ForMember(dest => dest.FormularioSessaoCampoId, opt => opt.MapFrom(src => src.FormularioSessaoCampoId));


            CreateMap<FormularioSessaoCampo, CriarFormularioSessaoCampoInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCampo, opt => opt.MapFrom(src => src.NomeCampo));

            CreateMap<FormularioSessaoCampoViewModel, CriarFormularioSessaoCampoInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCampo, opt => opt.MapFrom(src => src.NomeCampo));

            CreateMap<FormularioSessaoCampo, FormularioSessaoCampoInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCampo, opt => opt.MapFrom(src => src.NomeCampo));
        }
    }
}
