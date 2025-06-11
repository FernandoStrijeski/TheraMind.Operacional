using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseGAutomapperProfile : Profile
    {
        public ModeloAnamneseGAutomapperProfile()
        {
            CreateMap<ModeloAnamneseG, ModeloAnamneseGViewModel>().ReverseMap()
                .ForMember(dest => dest.ModeloAnamneseGid, opt => opt.MapFrom(src => src.ModeloAnamneseGid));

            CreateMap<ModeloAnamneseG, CriarModeloAnamneseGInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<ModeloAnamneseGViewModel, CriarModeloAnamneseGInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<ModeloAnamneseG, ModeloAnamneseGInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
