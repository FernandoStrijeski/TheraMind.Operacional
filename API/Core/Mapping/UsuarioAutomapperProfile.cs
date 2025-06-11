using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class UsuarioAutomapperProfile : Profile
    {
        public UsuarioAutomapperProfile()
        {            
            CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.UsuarioId, opt => opt.MapFrom(src => src.UsuarioId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.SenhaHash, opt => opt.MapFrom(src => src.SenhaHash))
                .ForMember(dest => dest.TrocaSenhaProximoAcesso, opt => opt.MapFrom(src => src.TrocaSenhaProximoAcesso))
                .ForMember(dest => dest.PerfilAcesso, opt => opt.MapFrom(src => src.PerfilAcesso));


            CreateMap<Usuario, CriarUsuarioInputModel>().ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<UsuarioViewModel, CriarUsuarioInputModel>().ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<Usuario, UsuarioInputModel>().ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
        }
    }
}
