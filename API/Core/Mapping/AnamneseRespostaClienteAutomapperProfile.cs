using API.modelos.InputModels;
using API.Operacional.modelos.ViewModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class AnamneseRespostaClienteAutomapperProfile : Profile
    {
        public AnamneseRespostaClienteAutomapperProfile()
        {
            CreateMap<AnamneseRespostaCliente, AnamneseRespostaClienteViewModel>()
                .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom(src => src.EmpresaId))
                .ForMember(dest => dest.FilialId, opt => opt.MapFrom(src => src.FilialId))
                .ForMember(dest => dest.AnamneseGrupoId, opt => opt.MapFrom(src => src.AnamneseGrupoId))
                .ForMember(dest => dest.AnamneseSubGrupoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoId))
                .ForMember(dest => dest.AnamneseSubGrupoQuestaoId, opt => opt.MapFrom(src => src.AnamneseSubGrupoQuestaoId))
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.ClienteId))
                .ForMember(dest => dest.Resposta, opt => opt.MapFrom(src => src.Resposta))               
                .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => src.DataCriacao));

            CreateMap<AnamneseRespostaCliente, CriarAnamneseRespostaClienteInputModel>().ReverseMap();

            CreateMap<AnamneseRespostaClienteViewModel, CriarAnamneseRespostaClienteInputModel>().ReverseMap();

            CreateMap<AnamneseRespostaCliente, AnamneseRespostaClienteInputModel>().ReverseMap();
        }
    }
}
