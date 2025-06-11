using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseSgAutomapperProfile : Profile
    {
        public ModeloAnamneseSgAutomapperProfile()
        {
            CreateMap<ModeloAnamneseSg, ModeloAnamneseSgViewModel>().ReverseMap()
                .ForMember(dest => dest.ModeloAnamneseSgid, opt => opt.MapFrom(src => src.ModeloAnamneseSgid));

            CreateMap<ModeloAnamneseSg, CriarModeloAnamneseSgInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<ModeloAnamneseSgViewModel, CriarModeloAnamneseSgInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<ModeloAnamneseSg, ModeloAnamneseSgInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
