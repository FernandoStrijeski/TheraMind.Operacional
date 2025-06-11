using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseSgQuestaoOAutomapperProfile : Profile
    {
        public ModeloAnamneseSgQuestaoOAutomapperProfile()
        {
            CreateMap<ModeloAnamneseSgQuestaoO, ModeloAnamneseSgQuestaoOViewModel>().ReverseMap()
                .ForMember(dest => dest.ModeloAnamneseSgQuestaoOid, opt => opt.MapFrom(src => src.ModeloAnamneseSgQuestaoOid));

            CreateMap<ModeloAnamneseSgQuestaoO, CriarModeloAnamneseSgQuestaoOInputModel>().ReverseMap()
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto));

            CreateMap<ModeloAnamneseSgQuestaoOViewModel, CriarModeloAnamneseSgQuestaoOInputModel>().ReverseMap()
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto));

            CreateMap<ModeloAnamneseSgQuestaoO, ModeloAnamneseSgQuestaoOInputModel>().ReverseMap()
                .ForMember(dest => dest.Texto, opt => opt.MapFrom(src => src.Texto));
        }
    }
}
