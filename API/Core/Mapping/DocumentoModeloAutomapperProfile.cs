using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class DocumentoModeloAutomapperProfile : Profile
    {
        public DocumentoModeloAutomapperProfile()
        {
            CreateMap<DocumentoModelo, DocumentoModeloViewModel>()
                .ForMember(dest => dest.DocumentoModeloId, opt => opt.MapFrom(src => src.DocumentoModeloId))
                .ForMember(dest => dest.TipoDocumentoId, opt => opt.MapFrom(src => src.TipoDocumentoId));
        }
    }
}
