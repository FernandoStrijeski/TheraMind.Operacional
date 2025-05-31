using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AnamneseGrupo
    {
        public AnamneseGrupo()
        {
            AnamneseRespostaClientes = new HashSet<AnamneseRespostaCliente>();
            AnamneseSubGrupoQuestaoOpcoes = new HashSet<AnamneseSubGrupoQuestaoOpcao>();
            AnamneseSubGrupoQuestoes = new HashSet<AnamneseSubGrupoQuestao>();
            AnamneseSubGrupos = new HashSet<AnamneseSubGrupo>();
        }

        [Key]
        public int AnamneseGrupoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<AnamneseRespostaCliente> AnamneseRespostaClientes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestaoOpcao> AnamneseSubGrupoQuestaoOpcoes { get; set; }
        public virtual ICollection<AnamneseSubGrupoQuestao> AnamneseSubGrupoQuestoes { get; set; }
        public virtual ICollection<AnamneseSubGrupo> AnamneseSubGrupos { get; set; }

        public static AnamneseGrupo CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, string titulo, bool? privado, bool editadoPorTodos, bool? ativo)
        {
            var modeloAnamneseG = new AnamneseGrupo
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ProfissionalId = profissionalID,
                Titulo = titulo,
                Privado = privado,
                EditadoPorTodos = editadoPorTodos,
                Ativo = ativo
            };
            return modeloAnamneseG;
        }

        public AnamneseGrupo AtualizarPropriedades(Guid empresaID, int filialID, Guid profissionalID, string titulo, bool? privado, bool editadoPorTodos, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ProfissionalId = profissionalID;

            Titulo = titulo;

            if (privado != null)
                Privado = privado;

            EditadoPorTodos = editadoPorTodos;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
