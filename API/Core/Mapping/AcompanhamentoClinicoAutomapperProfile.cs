using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AcompanhamentoClinicoAutomapperProfile : Profile
    {
        public AcompanhamentoClinicoAutomapperProfile()
        {
            CreateMap<AcompanhamentoClinico, AcompanhamentoClinicoViewModel>()
                .ForMember(dest => dest.AcompanhamentoClinicoId, opt => opt.MapFrom(src => src.AcompanhamentoClinicoId));
        }
    }
}
