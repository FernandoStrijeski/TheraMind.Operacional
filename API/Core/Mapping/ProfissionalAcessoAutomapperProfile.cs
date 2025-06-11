using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ProfissionalAcessoAutomapperProfile : Profile
    {
        public ProfissionalAcessoAutomapperProfile()
        {            
            CreateMap<ProfissionalAcesso, ProfissionalAcessoViewModel>().ReverseMap()
                .ForMember(dest => dest.ProfissionalAcessoId, opt => opt.MapFrom(src => src.ProfissionalAcessoId))
                .ForMember(dest => dest.AcessoTipo, opt => opt.MapFrom(src => src.AcessoTipo));

            CreateMap<ProfissionalAcesso, CriarProfissionalAcessoInputModel>().ReverseMap();

            CreateMap<ProfissionalAcessoViewModel, CriarProfissionalAcessoInputModel>().ReverseMap();

            CreateMap<ProfissionalAcesso, ProfissionalAcessoInputModel>().ReverseMap();
        }
    }
}
