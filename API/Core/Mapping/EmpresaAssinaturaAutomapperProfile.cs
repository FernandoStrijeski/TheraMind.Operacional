using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EmpresaAssinaturaAutomapperProfile : Profile
    {
        public EmpresaAssinaturaAutomapperProfile()
        {
            CreateMap<EmpresaAssinatura, EmpresaAssinaturaViewModel>().ReverseMap()
                .ForMember(dest => dest.EmpresaAssinaturaId, opt => opt.MapFrom(src => src.EmpresaAssinaturaId));

            CreateMap<EmpresaAssinatura, CriarEmpresaAssinaturaInputModel>().ReverseMap();

            CreateMap<EmpresaAssinaturaViewModel, CriarEmpresaAssinaturaInputModel>().ReverseMap();

            CreateMap<EmpresaAssinatura, EmpresaAssinaturaInputModel>().ReverseMap();
        }
    }
}
