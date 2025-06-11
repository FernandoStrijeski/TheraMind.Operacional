using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class ServicoAutomapperProfile : Profile
    {
        public ServicoAutomapperProfile()
        {            
            CreateMap<Servico, ServicoViewModel>().ReverseMap()
                .ForMember(dest => dest.ServicoId, opt => opt.MapFrom(src => src.ServicoId))
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Servico, CriarServicoInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<ServicoViewModel, CriarServicoInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));

            CreateMap<Servico, ServicoInputModel>().ReverseMap()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome));
        }
    }
}
