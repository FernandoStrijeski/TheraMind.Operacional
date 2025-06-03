using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseSgQuestaoOAutomapperProfile : Profile
    {
        public ModeloAnamneseSgQuestaoOAutomapperProfile()
        {
            CreateMap<ModeloAnamneseSgQuestaoO, ModeloAnamneseSgQuestaoOViewModel>()
                .ForMember(dest => dest.ModeloAnamneseSgQuestaoOid, opt => opt.MapFrom(src => src.ModeloAnamneseSgQuestaoOid));
        }
    }
}
