using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EmpresaAssinaturaAutomapperProfile : Profile
    {
        public EmpresaAssinaturaAutomapperProfile()
        {
            CreateMap<EmpresaAssinatura, EmpresaAssinaturaViewModel>()
                .ForMember(dest => dest.EmpresaAssinaturaId, opt => opt.MapFrom(src => src.EmpresaAssinaturaId));
        }
    }
}
