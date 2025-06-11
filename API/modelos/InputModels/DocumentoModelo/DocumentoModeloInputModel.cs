using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class DocumentoModeloInputModel
    {
        /// <summary>
        /// Id do modelo de documento
        /// </summary>
        public int DocumentoModeloId { get; set; }

        /// <summary>
        /// Id do tipo de documento
        /// </summary>
        public int TipoDocumentoId { get; set; }

        /// <summary>
        /// Título do documento
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Tipo do conteúdo do documento. Ex.: 0-Editável 1-Arquivo
        /// </summary>
        public short ConteudoTipo { get; set; }

        /// <summary>
        /// Conteúdo do documento, pode ser um texto editável ou o base64 de um arquivo
        /// </summary>
        public string Conteudo { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class DocumentoModeloValidator : AbstractValidator<DocumentoModeloInputModel>
    {
        public DocumentoModeloValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(100);            
        }
    }
}
