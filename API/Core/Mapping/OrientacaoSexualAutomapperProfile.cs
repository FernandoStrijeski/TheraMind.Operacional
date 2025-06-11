using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class OrientacaoSexualAutomapperProfile : Profile
    {
        public OrientacaoSexualAutomapperProfile()
        {            
            CreateMap<OrientacaoSexual, OrientacaoSexualViewModel>().ReverseMap()
                .ForMember(dest => dest.OrientacaoSexualId, opt => opt.MapFrom(src => src.OrientacaoSexualId))
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));


            CreateMap<OrientacaoSexual, CriarOrientacaoSexualInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<OrientacaoSexualViewModel, CriarOrientacaoSexualInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));

            CreateMap<OrientacaoSexual, OrientacaoSexualInputModel>().ReverseMap()
                .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        }
    }
}
