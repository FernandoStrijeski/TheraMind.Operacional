using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class DocumentoModeloAutomapperProfile : Profile
    {
        public DocumentoModeloAutomapperProfile()
        {
            CreateMap<DocumentoModelo, DocumentoModeloViewModel>().ReverseMap()
                .ForMember(dest => dest.DocumentoModeloId, opt => opt.MapFrom(src => src.DocumentoModeloId))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId));

            CreateMap<DocumentoModelo, CriarDocumentoModeloInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<DocumentoModeloViewModel, CriarDocumentoModeloInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<DocumentoModelo, DocumentoModeloInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
