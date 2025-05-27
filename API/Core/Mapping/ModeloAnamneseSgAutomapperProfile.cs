using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseSgAutomapperProfile : Profile
    {
        public ModeloAnamneseSgAutomapperProfile()
        {
            CreateMap<ModeloAnamneseSg, ModeloAnamneseSgViewModel>()
                .ForMember(dest => dest.ModeloAnamneseSgid, opt => opt.MapFrom(src => src.ModeloAnamneseSgid));
        }
    }
}
