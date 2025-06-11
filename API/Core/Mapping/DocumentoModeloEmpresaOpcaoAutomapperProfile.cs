using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;
using System.DirectoryServices;

namespace API.Core.Mapping
{
    public class DocumentoModeloEmpresaOpcaoAutomapperProfile : Profile
    {
        public DocumentoModeloEmpresaOpcaoAutomapperProfile()
        {
            CreateMap<DocumentoModeloEmpresaOpcao, DocumentoModeloEmpresaOpcaoViewModel>().ReverseMap()
                .ForMember(dest => dest.DocumentoModeloEmpresaOpcaoId, opt => opt.MapFrom(src => src.DocumentoModeloEmpresaOpcaoId));

            CreateMap<DocumentoModeloEmpresaOpcao, CriarDocumentoModeloEmpresaOpcaoInputModel>().ReverseMap();

            CreateMap<DocumentoModeloEmpresaOpcaoViewModel, CriarDocumentoModeloEmpresaOpcaoInputModel>().ReverseMap();

            CreateMap<DocumentoModeloEmpresaOpcao, DocumentoModeloEmpresaOpcaoInputModel>().ReverseMap();
        }
    }
}
