using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class NacionalidadeAutomapperProfile : Profile
    {
        public NacionalidadeAutomapperProfile()
        {            
            CreateMap<Nacionalidade, NacionalidadeViewModel>().ReverseMap()
                .ForMember(dest => dest.NacionalidadeId, opt => opt.MapFrom(src => src.NacionalidadeID))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Nacionalidade, CriarNacionalidadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<NacionalidadeViewModel, CriarNacionalidadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<Nacionalidade, NacionalidadeInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
