using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class IdentidadeGeneroAutomapperProfile : Profile
    {
        public IdentidadeGeneroAutomapperProfile()
        {            
            CreateMap<IdentidadeGenero, IdentidadeGeneroViewModel>()
                .ForMember(dest => dest.IdentidadeGeneroId, opt => opt.MapFrom(src => src.IdentidadeGeneroId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
