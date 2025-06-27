using API.Operacional.DTO.Empresa;
using API.Operacional.modelos.ViewModels;
using API.modelos.InputModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EmpresaAutomapperProfile : Profile
    {
        public EmpresaAutomapperProfile()
        {
            CreateMap<EmpresaDTO, Empresa>();
            CreateMap<CriarEmpresaInputModel, Empresa>();
            CreateMap<EmpresaDTO, CriarEmpresaInputModel>();
            CreateMap<EmpresaInputModel, Empresa>().ReverseMap();

            CreateMap<Empresa, EmpresaViewModel>()
                .ForMember(dest => dest.Logotipo, opt => opt.MapFrom(src => src.Logotipo));
        }
    }
}
