using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class FormularioSessaoCampo
    {
        [Key]
        public int FormularioSessaoCampoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public string NomeCampo { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual FormularioSessao FormularioSessao { get; set; } = null!;
        public virtual Servico Servico { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }

        public static FormularioSessaoCampo CriarParaImportacao(Guid empresaID, int filialID, int servicoID, int formularioSessaoID, string nomeCampo, bool? ativo)
        {
            var formularioSessao = new FormularioSessaoCampo
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ServicoId = servicoID,
                FormularioSessaoId = formularioSessaoID,
                NomeCampo = nomeCampo,
                Ativo = ativo
            };
            return formularioSessao;
        }

        public FormularioSessaoCampo AtualizarPropriedades(Guid empresaID, int filialID, int servicoID, int formularioSessaoID, string nomeCampo, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ServicoId = servicoID;
            FormularioSessaoId = formularioSessaoID;
            NomeCampo = nomeCampo;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
