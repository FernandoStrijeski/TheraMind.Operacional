using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class DocumentoModeloInputModel
    {
        public int DocumentoModeloId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short ConteudoTipo { get; set; }
        public string? ConteudoTexto { get; set; }
        public byte[]? ConteudoArquivo { get; set; }
        public bool? Ativo { get; set; }
    }

    public class AtualizaDocumentoModeloInputModelValidator : AbstractValidator<DocumentoModeloInputModel>
    {
        public AtualizaDocumentoModeloInputModelValidator()
        {
            RuleFor(x => x.TipoDocumentoId)
            .GreaterThan(0)
            .WithMessage("O tipo de documento é obrigatório.");

            RuleFor(x => x.Titulo)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Informe o título do documento.");

            RuleFor(x => x.ConteudoTipo)
                .InclusiveBetween((short)0, (short)1)
                .WithMessage("O tipo de conteúdo é inválido.");

            When(x => x.ConteudoTipo == 0, () =>
            {
                RuleFor(x => x.ConteudoTexto)
                    .NotEmpty()
                    .WithMessage("O conteúdo do documento é obrigatório.");
            });

            When(x => x.ConteudoTipo == 1, () =>
            {
                RuleFor(x => x.ConteudoArquivo)
                    .NotNull()
                    .Must(x => x.Length > 0)
                    .WithMessage("O arquivo PDF é obrigatório.");
            });

        }
    }
}
