using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Auditoria
    {
        [Key]
        public Guid AuditoriaId { get; set; }
        public DateTime? DataHoraExecucao { get; set; }
        public Guid? EmpresaId { get; set; }
        public int? FilialId { get; set; }
        public short TipoAcao { get; set; }
        public string? AcaoExecutada { get; set; }
        public Guid UsuarioId { get; set; }
        public string PerfilAcesso { get; set; } = null!;
        public string Ipacesso { get; set; } = null!;

        public virtual Empresa? Empresa { get; set; }
        public virtual Filial? Filial { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
    }
}
