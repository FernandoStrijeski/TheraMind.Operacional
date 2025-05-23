using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Servico
    {
        public Servico()
        {
            AgendaSessaoItens = new HashSet<AgendaSessaoItem>();
            AgendaSessoes = new HashSet<AgendaSessao>();
            FormularioSessaoCampos = new HashSet<FormularioSessaoCampo>();
            FormularioSessoes = new HashSet<FormularioSessao>();
        }

        [Key]
        public int ServicoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string Nome { get; set; } = null!;
        public bool Padrao { get; set; }
        public short? DuracaoMinutos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItens { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessoes { get; set; }
        public virtual ICollection<FormularioSessaoCampo> FormularioSessaoCampos { get; set; }
        public virtual ICollection<FormularioSessao> FormularioSessoes { get; set; }

        public static Servico CriarParaImportacao(Guid empresaID, int filialID, string nome, bool padrao, short? duracaoMinutos, bool? ativo)
        {
            var servico = new Servico
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                Nome = nome,
                Padrao = padrao,
                DuracaoMinutos = duracaoMinutos,
                Ativo = ativo
            };
            return servico;
        }

        public Servico AtualizarPropriedades(Guid empresaID, int filialID, string nome, bool padrao, short? duracaoMinutos, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            Nome = nome;
            Padrao = padrao;

            if (ativo != null)
                DuracaoMinutos = duracaoMinutos;
                
            if (ativo != null)
                Ativo = ativo;

            return this;
        }

    }
}
