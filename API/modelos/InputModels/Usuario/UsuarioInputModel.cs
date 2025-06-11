using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace API.modelos.InputModels
{
    public class UsuarioInputModel
    {
        /// <summary>
        /// ID do usuário
        /// </summary>
        [Required]
        public Guid UsuarioId { get; set; }

        /// <summary>
        /// ID da Empresa opcional
        /// </summary>
        public Guid? EmpresaId { get; set; }

        /// <summary>
        /// ID da Filial opcional
        /// </summary>
        public int? FilialId { get; set; }

        /// <summary>
        /// E-mail do usuário para login
        /// </summary>
        [Required]
        public string Email { get; set; } = null!;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required]
        public string SenhaHash { get; set; } = null!;

        /// <summary>
        /// Trocar a senha no próximo acesso
        /// </summary>
        [Required]
        public bool TrocaSenhaProximoAcesso { get; set; }

        /// <summary>
        /// Perfil de acesso "ADMIN, GESTOR, PROFISSIONAL, CLIENTE"
        /// </summary>
        [Required]
        public string PerfilAcesso { get; set; } = null!;   

        /// <summary>
        /// Ativo Sim ou Não
        /// </summary> 
        public bool Ativo { get; set; }

    }

    public class UsuarioValidator : AbstractValidator<UsuarioInputModel>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Email).MaximumLength(255);
        }
    }
}
