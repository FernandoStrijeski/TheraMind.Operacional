using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class FilialInputModel
    {
        /// <summary>
        /// Identificador da Filial
        /// </summary>
        [Required]
        public int FilialId { get; set; }

        /// <summary>
        /// Identificador da empresa
        /// </summary>
        [Required]
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Código Único da Filial para Login
        /// </summary>        
        public string CodigoUnico { get; set; }

        /// <summary>
        /// CPF quando Empresa Física
        /// </summary>        
        public string? Cpf { get; set; }

        /// <summary>
        /// CNPJ quando Empresa Jurídica
        /// </summary>
        public string? Cnpj { get; set; }

        /// <summary>
        /// Inscrição Estadual
        /// </summary>
        public string? InscricaoEstadual { get; set; }

        /// <summary>
        /// Inscrição Municipal
        /// </summary>
        public string? InscricaoMunicipal { get; set; }

        /// <summary>
        /// Nome da Filial
        /// </summary>
        [Required]
        public string NomeFilial { get; set; }

        /// <summary>
        /// Tipo de Logradouro
        /// </summary>
        public string? TipoLogradouroId { get; set; }

        /// <summary>
        /// Nome da rua
        /// </summary>
        public string? Endereco { get; set; }

        /// <summary>
        /// Número do endereço
        /// </summary>
        public short? Numero { get; set; }

        /// <summary>
        /// CEP da rua
        /// </summary>
        public string? Cep { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string? Complemento { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string? Bairro { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required]
        public int CidadeId { get; set; }

        /// <summary>
        /// Telefone da Filial
        /// </summary>
        public string? Telefone { get; set; }

        /// <summary>
        /// Ativo
        /// </summary>
        public bool? Ativo { get; set; }
    }

    public class FilialValidator : AbstractValidator<FilialInputModel>
    {
        public FilialValidator()
        {
            RuleFor(x => x.NomeFilial).MaximumLength(255);
        }
    }
}
