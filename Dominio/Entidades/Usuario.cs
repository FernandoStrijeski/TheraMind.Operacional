using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {            
        }

        [Key]
        public Guid UsuarioId { get; set; }
        public Guid? EmpresaId { get; set; }
        public int? FilialId { get; set; }
        public string Email { get; set; } = null!;
        public string SenhaHash { get; set; } = null!;
        public bool TrocaSenhaProximoAcesso { get; set; }
        public string PerfilAcesso { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa? Empresa { get; set; }
        public virtual Filial? Filial { get; set; }
        public virtual Profissional? Profissional { get; set; }
        public static Usuario CriarParaImportacao(Guid? empresaID, int? filialID, string email, string senhaHash, bool trocaSenhaProximoAcesso, string perfilAcesso, bool? ativo)
        {
            var usuario = new Usuario
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                Email = email,
                SenhaHash = senhaHash,
                TrocaSenhaProximoAcesso = trocaSenhaProximoAcesso,
                PerfilAcesso = perfilAcesso,
                Ativo = ativo
            };
            return usuario;
        }

        public Usuario AtualizarPropriedades(Guid? empresaID, int? filialID, string email, string senhaHash, bool trocaSenhaProximoAcesso, string perfilAcesso, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            Email = email;
            SenhaHash = senhaHash;
            TrocaSenhaProximoAcesso = trocaSenhaProximoAcesso;
            PerfilAcesso = perfilAcesso;
                
            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
