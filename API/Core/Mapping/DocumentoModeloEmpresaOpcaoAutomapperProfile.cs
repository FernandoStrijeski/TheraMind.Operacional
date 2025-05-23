using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class DocumentoModeloEmpresaOpcaoAutomapperProfile : Profile
    {
        public DocumentoModeloEmpresaOpcaoAutomapperProfile()
        {
            CreateMap<DocumentoModeloEmpresaOpcao, DocumentoModeloEmpresaOpcaoViewModel>()
                .ForMember(dest => dest.DocumentoModeloEmpresaOpcaoId, opt => opt.MapFrom(src => src.DocumentoModeloEmpresaOpcaoId));
        }
    }
}
