using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ModeloAnamneseSgQuestaoInputModel
    {
        /// <summary>
        /// Id da questão do subgrupo do grupo de modelo da anamnese
        /// </summary>
        public int ModeloAnamneseSgQuestaoId { get; set; }

        /// <summary>
        /// Id do grupo do modelo da anamnese
        /// </summary>
        public int ModeloAnamneseGid { get; set; }

        /// <summary>
        /// Id do subgrupo do modelo da anamnese
        /// </summary>
        public int ModeloAnamneseSgid { get; set; }
        
        /// <summary>
        /// Título do modelo de anamnese
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Tipo de Opção (0-Texto Curto, 1-Texto Longo, 2-SimNao, 3-Optativa, 4-MultiplaEscolha)
        /// </summary>
        public short TipoOpcao { get; set; }

        /// <summary>
        /// Ordem numérica de emissão
        /// </summary>
        public short Ordem { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class ModeloAnamneseSgQuestaoValidator : AbstractValidator<ModeloAnamneseSgQuestaoInputModel>
    {
        public ModeloAnamneseSgQuestaoValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(250);
        }
    }
}
