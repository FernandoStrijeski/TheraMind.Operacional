using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class DocumentoModeloEmpresaOpcaoInputModel
    {
        /// <summary>
        /// Id da opção para o modelo de documento da empresa
        /// </summary>
        public int DocumentoModeloEmpresaOpcaoId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Tipo de opção: 0-Assinatura 1-Marca d'agua
        /// </summary>
        public short TipoOpcao { get; set; }

        /// <summary>
        /// Conteúdo da opção dos documentos em base64 (Logotipo ou marca d'agua)
        /// </summary>
        public string ConteudoBase64 { get; set; }

        /// <summary>
        /// Percentual de transparência quando tipo 1-Marca d'agua
        /// </summary>
        public decimal? Transparencia { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class DocumentoModeloEmpresaOpcaoValidator : AbstractValidator<DocumentoModeloEmpresaOpcaoInputModel>
    {
        public DocumentoModeloEmpresaOpcaoValidator()
        {
                
        }
    }
}
