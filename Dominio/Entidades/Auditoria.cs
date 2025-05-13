using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public enum eTipoAcao
    {
        Inclusao = 1,
        Edicao = 2,
        Exclusao = 3,
        Consulta = 4,
        Acesso = 5,
    }

    public partial class Auditoria
    {
        public Auditoria()
        {
            AuditoriaId = Guid.NewGuid();
        }

        [Key]
        public Guid AuditoriaId { get; set; }
        public DateTime? DataHoraExecucao { get; set; }
        public Guid? EmpresaId { get; set; }
        public int? FilialId { get; set; }
        public eTipoAcao TipoAcao { get; set; } //-- 1-inclusao, 2-edição, 3-exclusão, 4-consulta, 5-Acesso
        public string? AcaoExecutada { get; set; }
        public Guid UsuarioID { get; set; }
        public string PerfilAcesso { get; set; } = null!; //-- ADMIN, GESTOR, PROFISSIONAL, CLIENTE
        public string IPAcesso { get; set; } = null!;

        public virtual Empresa? Empresa { get; set; }
        public virtual Filial? Filial { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
