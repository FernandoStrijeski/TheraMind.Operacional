using System.ComponentModel.DataAnnotations;
using BoletoNetCore;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class CriarBoletoInputModel
    {
        [Required]
        public Bancos Banco {  get; set; }

        [Required]
        public BeneficiarioInputModel Beneficiario { get; set; }

        public BoletoInputModel Boleto { get; set; }

        public class BoletoInputModel
        {
            [Required]
            public PagadorInputModel Pagador { get; set; }
            [Required]
            public DateTime DataVencimento { get; set; }
            [Required]
            public decimal ValorTitulo { get; set; }
            [Required]
            public string NossoNumero { get; set; }
            [Required]
            public string NumeroDocumento { get; set; }
            [Required]
            public TipoEspecieDocumento EspecieDocumento { get; set; }
            [Required]
            public string CodigoMotivoOcorrencia { get; set; }

            public class PagadorInputModel
            {
                [Required]
                public string Nome { get; set; }
                [Required]
                public string CPFCNPJ { get; set; }
                [Required]
                public EnderecoInputModel Endereco { get; set; }

                public class EnderecoInputModel
                {
                    [Required]
                    public string LogradouroEndereco { get; set; }
                    [Required]
                    public string LogradouroNumero { get; set; }
                    [Required]
                    public string Bairro { get; set; }
                    [Required]
                    public string Cidade { get; set; }
                    [Required]
                    public string UF { get; set; }
                    [Required]
                    public string CEP { get; set; }
                }
            }
        }

        public class BeneficiarioInputModel
        {
            [Required]
            public string CPFCNPJ { get; set; }
            [Required]
            public string Nome { get; set; }
            [Required]
            public string Codigo { get; set; }
            [Required]
            public ContaBancariaInputModel ContaBancaria { get; set; }

            public class ContaBancariaInputModel
            {
                [Required]
                public string Agencia { get; set; }
                [Required]
                public string DigitoAgencia { get; set; }
                [Required]
                public string Conta { get; set; }
                [Required]
                public string DigitoConta { get; set; }
                public string CarteiraPadrao { get; set; }
                [Required]
                public string VariacaoCarteiraPadrao { get; set; } // necessário sicredi!
                [Required]
                public string OperacaoConta { get; set; } // valor fictício – confirme com seu gerente ou manual do banco
                [Required]
                public TipoCarteira TipoCarteiraPadrao { get; set; }
                [Required]
                public TipoFormaCadastramento TipoFormaCadastramento { get; set; }
            }
        }
    }
    

    public class CriarBoletoInputModelValidator : AbstractValidator<CriarBoletoInputModel>
    {
        public CriarBoletoInputModelValidator()
        {
            RuleFor(x => x.Beneficiario.CPFCNPJ).MaximumLength(14);
            RuleFor(x => x.Beneficiario.Codigo).MaximumLength(5);
        }
    }
}
