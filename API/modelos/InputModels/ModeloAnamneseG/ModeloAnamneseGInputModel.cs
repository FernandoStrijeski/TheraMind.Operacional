using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class ModeloAnamneseGInputModel
    {
        /// <summary>
        /// Id do modelo de anamnese
        /// </summary>
        public int ModeloAnamneseGid { get; set; }
        
        /// <summary>
        /// Título do modelo de anamnese
        /// </summary>
        public string Titulo { get; set; } = null!;

        /// <summary>
        /// Exibição do modelo de anamnese (Privado para o criador ou para todos da empresa)
        /// </summary>
        public bool? Privado { get; set; }

        /// <summary>
        /// Permite que todos possam editar ou apenas o criador do modelo
        /// </summary>
        public bool EditadoPorTodos { get; set; }

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

        /// <summary>
        /// Data de criação do registro
        /// </summary> 
        public DateTime DataCriacao { get; set; }
    }

    public class ModeloAnamneseGValidator : AbstractValidator<ModeloAnamneseGInputModel>
    {
        public ModeloAnamneseGValidator()
        {
            RuleFor(x => x.Titulo).MaximumLength(100);            
        }
    }
}
