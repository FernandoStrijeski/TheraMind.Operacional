using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class EscolaridadeAutomapperProfile : Profile
    {
        public EscolaridadeAutomapperProfile()
        {            
            CreateMap<Escolaridade, EscolaridadeViewModel>().ReverseMap()
                .ForMember(dest => dest.EscolaridadeId, opt => opt.MapFrom(src => src.EscolaridadeId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Escolaridade, CriarEscolaridadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<EscolaridadeViewModel, CriarEscolaridadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Escolaridade, EscolaridadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
