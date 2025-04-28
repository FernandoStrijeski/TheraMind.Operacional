using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class OrientacaoSexualAutomapperProfile : Profile
    {
        public OrientacaoSexualAutomapperProfile()
        {            
            CreateMap<OrientacaoSexual, OrientacaoSexualViewModel>()
                .ForMember(dest => dest.OrientacaoSexualId, opt => opt.MapFrom(src => src.OrientacaoSexualId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
