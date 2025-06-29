using API.Operacional.DTO;
using API.Operacional.DTO.Empresa;
using API.Operacional.modelos.ViewModels;
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

            CreateMap<FilialInputModel, FilialViewModel>().ReverseMap();
            CreateMap<Filial, FilialInputModel>().ReverseMap();
            CreateMap<Filial, FilialViewModel>().ReverseMap();
        }
    }
}
