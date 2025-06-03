using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class DocumentoVariavelAutomapperProfile : Profile
    {
        public DocumentoVariavelAutomapperProfile()
        {
            CreateMap<DocumentoVariavel, DocumentoVariavelViewModel>()
                .ForMember(dest => dest.DocumentoVariavelId, opt => opt.MapFrom(src => src.DocumentoVariavelId));
        }
    }
}
