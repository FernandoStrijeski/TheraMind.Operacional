using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class DocumentoVariavelInputModel
    {
        /// <summary>
        /// Id da variável dos documentos
        /// </summary>
        public int DocumentoVariavelId { get; set; }

        /// <summary>
        /// Nome da variável dos documentos
        /// </summary>
        public string NomeVariavel { get; set; } = null!;

        /// <summary>
        /// Nome do campo do documento
        /// </summary>
        public string NomeCampo { get; set; } = null!;

        /// <summary>
        /// Nome da tabela relacionada do banco
        /// </summary>
        public string NomeTabela { get; set; } = null!;        

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }
    }

    public class DocumentoVariavelValidator : AbstractValidator<DocumentoVariavelInputModel>
    {
        public DocumentoVariavelValidator()
        {
            RuleFor(x => x.NomeVariavel).MaximumLength(250);            
        }
    }
}
