using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class DocumentoModeloEmpresaOpcao
    {
        [Key]
        public int DocumentoModeloEmpresaOpcaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short TipoOpcao { get; set; }
        public string Conteudo { get; set; } = null!;
        public decimal? Transparencia { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
    }
}
