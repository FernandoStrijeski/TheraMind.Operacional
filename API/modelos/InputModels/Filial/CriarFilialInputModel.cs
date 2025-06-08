using System.ComponentModel.DataAnnotations;
using Dominio.Core.Utils;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarFilialInputModel
    {
        public int FilialId { get; set; }
        public Guid EmpresaId { get; set; }
        public string CodigoUnico { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? InscricaoMunicipal { get; set; }
        public string NomeFilial { get; set; }
        public string? TipoLogradouroId { get; set; }
        public string? Endereco { get; set; }
        public short? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public int CidadeId { get; set; }
        public string? Telefone { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class CriarFilialInputModelValidator : AbstractValidator<CriarFilialInputModel>
    {
        public CriarFilialInputModelValidator()
        {
            RuleFor(x => x.NomeFilial).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe o nome da Filial!");
            RuleFor(x => x.CodigoUnico).NotEmpty().MaximumLength(30).WithMessage("Por favor, informe o código único da Filial!");
        }
    }
}
