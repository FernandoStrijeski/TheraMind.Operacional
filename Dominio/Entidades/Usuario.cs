using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Usuario
    {
        public Usuario()
        {
            Auditoria = new HashSet<Auditoria>();
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
        public virtual ICollection<Auditoria> Auditoria { get; set; }
    }
}
