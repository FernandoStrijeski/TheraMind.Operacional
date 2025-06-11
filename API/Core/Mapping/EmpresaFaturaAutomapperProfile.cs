using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EmpresaFaturaAutomapperProfile : Profile
    {
        public EmpresaFaturaAutomapperProfile()
        {
            CreateMap<EmpresaFatura, EmpresaFaturaViewModel>().ReverseMap()
                .ForMember(dest => dest.EmpresaFaturaId, opt => opt.MapFrom(src => src.EmpresaFaturaId));


            CreateMap<EmpresaFatura, CriarEmpresaFaturaInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EmpresaFaturaViewModel, CriarEmpresaFaturaInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EmpresaFatura, EmpresaFaturaInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
