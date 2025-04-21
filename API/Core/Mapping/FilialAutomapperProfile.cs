using API.AdmissaoDigital.DTO;
using API.AdmissaoDigital.DTO.Empresa;
using API.AdmissaoDigital.modelos.ViewModels;
using API.modelos.InputModels;
using AutoMapper;
using Dominio.Entidades;

namespace API.Core.Mapping
{
    public class FilialAutomapperProfile : Profile
    {
        public FilialAutomapperProfile()
        {
            CreateMap<FilialDTO, Filial>();
            CreateMap<CriarFilialInputModel, Filial>();
            CreateMap<FilialDTO, CriarFilialInputModel>();

            CreateMap<Filial, FilialViewModel>();
        }
    }
}
