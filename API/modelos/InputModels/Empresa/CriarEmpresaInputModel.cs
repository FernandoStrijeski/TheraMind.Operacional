using System.ComponentModel.DataAnnotations;
using Dominio.Core.Utils;
using FluentValidation;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.modelos.InputModels
{
    [JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
    public class CriarEmpresaInputModel
    {
        public Guid EmpresaID { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public byte[]? Logotipo { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class CriarEmpresaInputModelValidator : AbstractValidator<CriarEmpresaInputModel>
    {
        public CriarEmpresaInputModelValidator()
        {
            RuleFor(x => x.RazaoSocial).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe a Razão Social!");
            RuleFor(x => x.NomeFantasia).NotEmpty().MaximumLength(255).WithMessage("Por favor, informe o nome fantasia!");
        }
    }
}
