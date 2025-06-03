using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Intrinsics.Arm;

namespace Dominio.Entidades
{
    public partial class AnamneseRespostaCliente
    {
        [Key]
        public Guid EmpresaId { get; set; }
        [Key]
        public int FilialId { get; set; }
        [Key]
        public Guid ProfissionalId { get; set; }
        [Key]
        public int AnamneseGrupoId { get; set; }
        [Key]
        public int AnamneseSubGrupoId { get; set; }
        [Key]
        public int AnamneseSubGrupoQuestaoId { get; set; }
        [Key]
        public Guid ClienteId { get; set; }
        public string? Resposta { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual AnamneseGrupo AnamneseGrupo { get; set; } = null!;
        public virtual AnamneseSubGrupo AnamneseSubGrupo { get; set; } = null!;
        public virtual AnamneseSubGrupoQuestao AnamneseSubGrupoQuestao { get; set; } = null!;
        public virtual Cliente Cliente { get; set; } = null!;

        public static AnamneseRespostaCliente CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID, int anamneseSubGrupoQuestaoID, Guid clienteID, string? resposta)
        {
            var anamneseSubGrupoQuestao = new AnamneseRespostaCliente
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ProfissionalId = profissionalID,
                AnamneseGrupoId = anamneseGrupoID,
                AnamneseSubGrupoId = anamneseSubGrupoID,
                AnamneseSubGrupoQuestaoId = anamneseSubGrupoQuestaoID,
                ClienteId = clienteID,
                Resposta = resposta
            };
            return anamneseSubGrupoQuestao;
        }

        public AnamneseRespostaCliente AtualizarPropriedades(Guid empresaID, int filialID, Guid profissionalID, int anamneseGrupoID, int anamneseSubGrupoID, int anamneseSubGrupoQuestaoID, Guid clienteID, string? resposta)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ProfissionalId = profissionalID;
            AnamneseGrupoId = anamneseGrupoID;
            AnamneseSubGrupoId = anamneseSubGrupoID;
            AnamneseSubGrupoQuestaoId = anamneseSubGrupoQuestaoID;
            ClienteId = clienteID;

            if (resposta != null)
                Resposta = resposta;

            return this;
        }
    }
}
