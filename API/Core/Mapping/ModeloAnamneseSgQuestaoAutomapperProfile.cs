using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseSgQuestaoAutomapperProfile : Profile
    {
        public ModeloAnamneseSgQuestaoAutomapperProfile()
        {
            CreateMap<ModeloAnamneseSgQuestao, ModeloAnamneseSgQuestaoViewModel>().ReverseMap()
                .ForMember(dest => dest.ModeloAnamneseSgQuestaoId, opt => opt.MapFrom(src => src.ModeloAnamneseSgQuestaoId));


            CreateMap<ModeloAnamneseSgQuestao, CriarModeloAnamneseSgQuestaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<ModeloAnamneseSgQuestaoViewModel, CriarModeloAnamneseSgQuestaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));

            CreateMap<ModeloAnamneseSgQuestao, ModeloAnamneseSgQuestaoInputModel>().ReverseMap()
                .ForMember(dest => dest.Titulo, opt => opt.MapFrom(src => src.Titulo));
        }
    }
}
