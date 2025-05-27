using API.AdmissaoDigital.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ModeloAnamneseGAutomapperProfile : Profile
    {
        public ModeloAnamneseGAutomapperProfile()
        {
            CreateMap<ModeloAnamneseG, ModeloAnamneseGViewModel>()
                .ForMember(dest => dest.ModeloAnamneseGid, opt => opt.MapFrom(src => src.ModeloAnamneseGid));
        }
    }
}
