using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class DocumentoModeloEmpresaAutomapperProfile : Profile
    {
        public DocumentoModeloEmpresaAutomapperProfile()
        {
            CreateMap<DocumentoModeloEmpresa, DocumentoModeloEmpresaViewModel>().ReverseMap()
                .ForMember(dest => dest.DocumentoModeloEmpresaID, opt => opt.MapFrom(src => src.DocumentoModeloEmpresaID))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId));

            CreateMap<DocumentoModeloEmpresa, CriarDocumentoModeloEmpresaInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<DocumentoModeloEmpresaViewModel, CriarDocumentoModeloEmpresaInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<DocumentoModeloEmpresa, DocumentoModeloEmpresaInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
