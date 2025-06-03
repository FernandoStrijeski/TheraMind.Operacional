using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EmpresaFaturaAutomapperProfile : Profile
    {
        public EmpresaFaturaAutomapperProfile()
        {
            CreateMap<EmpresaFatura, EmpresaFaturaViewModel>()
                .ForMember(dest => dest.EmpresaFaturaId, opt => opt.MapFrom(src => src.EmpresaFaturaId));
        }
    }
}
