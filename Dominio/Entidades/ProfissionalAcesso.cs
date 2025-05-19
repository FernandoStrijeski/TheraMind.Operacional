using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ProfissionalAcesso
    {
        [Key]
        public int ProfissionalAcessoId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short AcessoTipo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;

        public static ProfissionalAcesso CriarParaImportacao(Guid profissionalID, Guid empresaID, int filialID, short acessoTipo)
        {
            var profissionalAcesso = new ProfissionalAcesso
            {
                ProfissionalId = profissionalID,
                EmpresaId = empresaID,
                FilialId = filialID,
                AcessoTipo = acessoTipo
            };
            return profissionalAcesso;
        }

        public ProfissionalAcesso AtualizarPropriedades(Guid profissionalID, Guid empresaID, int filialID, short acessoTipo)
        {
            ProfissionalId = profissionalID;
                EmpresaId = empresaID;
                FilialId = filialID;
                AcessoTipo = acessoTipo;

            return this;
        }

    }
}
