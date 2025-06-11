using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class IdentidadeGeneroAutomapperProfile : Profile
    {
        public IdentidadeGeneroAutomapperProfile()
        {
            CreateMap<IdentidadeGenero, IdentidadeGeneroViewModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<IdentidadeGenero, CriarIdentidadeGeneroInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<IdentidadeGeneroViewModel, CriarIdentidadeGeneroInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<IdentidadeGenero, IdentidadeGeneroInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
