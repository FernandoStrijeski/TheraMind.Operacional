using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseSgQuestaoAutomapperProfile : Profile
    {
        public ModeloAnamneseSgQuestaoAutomapperProfile()
        {
            CreateMap<ModeloAnamneseSgQuestao, ModeloAnamneseSgQuestaoViewModel>()
                .ForMember(dest => dest.ModeloAnamneseSgQuestaoId, opt => opt.MapFrom(src => src.ModeloAnamneseSgQuestaoId));
        }
    }
}
