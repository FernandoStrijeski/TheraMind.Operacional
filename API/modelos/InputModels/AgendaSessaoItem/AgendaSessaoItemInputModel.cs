using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class AgendaSessaoItemInputModel
    {
        /// <summary>
        /// Id do item da sessão na agenda
        /// </summary>
        public int AgendaSessaoItemId { get; set; }

        /// <summary>
        /// Id da empresa
        /// </summary>
        public Guid EmpresaId { get; set; }

        /// <summary>
        /// Id da filial
        /// </summary>
        public int FilialId { get; set; }

        /// <summary>
        /// Id do profissional responsável pela agenda
        /// </summary>
        public Guid ProfissionalId { get; set; }

        /// <summary>
        /// Id da agenda do profissional
        /// </summary>
        public int AgendaProfissionalId { get; set; }

        /// <summary>
        /// Id do serviço prestado
        /// </summary>
        public int ServicoId { get; set; }

        /// <summary>
        /// Id do formulário da sessão
        /// </summary>
        public int FormularioSessaoId { get; set; }

        /// <summary>
        /// Id do campo do formulário da sessão
        /// </summary>
        public int FormularioSessaoCampoId { get; set; }

        /// <summary>
        /// Id do cliente
        /// </summary>
        public Guid? ClienteId { get; set; }

        /// <summary>
        /// Id da sessão da agenda
        /// </summary>
        public Guid AgendaSessaoId { get; set; }

        /// <summary>
        /// Tipo de campo (0-Texto, 1-Anexo)
        /// </summary>
        public short CampoTipo { get; set; }

        /// <summary>
        /// Texto livre
        /// </summary>
        public string? ConteudoTexto { get; set; }

        /// <summary>
        /// Arquivo anexado
        /// </summary>
        public byte[]? ConteudoArquivo { get; set; }
        
        /// <summary>
        /// Situação do item
        /// </summary>
        public bool? Ativo { get; set; }

    }

    public class AgendaSessaoItemValidator : AbstractValidator<AgendaSessaoItemInputModel>
    {
        public AgendaSessaoItemValidator()
        {
                      
        }
    }
}
