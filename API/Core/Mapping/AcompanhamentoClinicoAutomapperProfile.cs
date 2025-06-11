using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AcompanhamentoClinicoAutomapperProfile : Profile
    {
        public AcompanhamentoClinicoAutomapperProfile()
        {
            CreateMap<AcompanhamentoClinico, AcompanhamentoClinicoViewModel>().ReverseMap()
                .ForMember(dest => dest.AcompanhamentoClinicoId, opt => opt.MapFrom(src => src.AcompanhamentoClinicoId));

            CreateMap<AcompanhamentoClinico, CriarAcompanhamentoClinicoInputModel>().ReverseMap();

            CreateMap<AcompanhamentoClinicoViewModel, CriarAcompanhamentoClinicoInputModel>().ReverseMap();

            CreateMap<AcompanhamentoClinico, AcompanhamentoClinicoInputModel>().ReverseMap();

        }
    }
}
