using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class DocumentoModeloEmpresaInputModel
    {
        /// <summary>
        /// Id do modelo de documento da empresa
        /// </summary>
        public int DocumentoModeloEmpresaId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

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

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class DocumentoModeloEmpresaValidator : AbstractValidator<DocumentoModeloEmpresaInputModel>
    {
        public DocumentoModeloEmpresaValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(100);            
        }
    }
}
