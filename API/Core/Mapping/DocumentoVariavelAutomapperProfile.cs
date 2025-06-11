using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class DocumentoVariavelAutomapperProfile : Profile
    {
        public DocumentoVariavelAutomapperProfile()
        {
            CreateMap<DocumentoVariavel, DocumentoVariavelViewModel>().ReverseMap()
                .ForMember(dest => dest.DocumentoVariavelId, opt => opt.MapFrom(src => src.DocumentoVariavelId));

            CreateMap<DocumentoVariavel, CriarDocumentoVariavelInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCampo, opt => opt.MapFrom(src => src.NomeCampo));

            CreateMap<DocumentoVariavelViewModel, CriarDocumentoVariavelInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCampo, opt => opt.MapFrom(src => src.NomeCampo));

            CreateMap<DocumentoVariavel, DocumentoVariavelInputModel>().ReverseMap()
                .ForMember(dest => dest.NomeCampo, opt => opt.MapFrom(src => src.NomeCampo));
        }
    }
}
