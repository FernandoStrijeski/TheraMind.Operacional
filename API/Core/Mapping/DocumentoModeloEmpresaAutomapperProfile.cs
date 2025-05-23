using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class DocumentoModeloEmpresaAutomapperProfile : Profile
    {
        public DocumentoModeloEmpresaAutomapperProfile()
        {
            CreateMap<DocumentoModeloEmpresa, DocumentoModeloEmpresaViewModel>()
                .ForMember(dest => dest.DocumentoModeloEmpresaID, opt => opt.MapFrom(src => src.DocumentoModeloEmpresaID))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId));
        }
    }
}
