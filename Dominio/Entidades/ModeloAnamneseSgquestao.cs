using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseSgQuestao
    {
        public ModeloAnamneseSgQuestao()
        {
            ModeloAnamneseSgQuestaoOs = new HashSet<ModeloAnamneseSgQuestaoO>();
        }

        [Key]
        public int ModeloAnamneseSgQuestaoId { get; set; }
        public int ModeloAnamneseGid { get; set; }
        public int ModeloAnamneseSgid { get; set; }
        public string Titulo { get; set; } = null!;
        public short TipoOpcao { get; set; }
        public short Ordem { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ModeloAnamneseG ModeloAnamneseG { get; set; } = null!;
        public virtual ModeloAnamneseSg ModeloAnamneseSg { get; set; } = null!;
        public virtual ICollection<ModeloAnamneseSgQuestaoO> ModeloAnamneseSgQuestaoOs { get; set; }
        public static ModeloAnamneseSgQuestao CriarParaImportacao(int modeloAnamneseGID, int modeloAnamneseSgID, string titulo, short tipoOpcao, short ordem, bool? ativo)
        {
            var modeloAnamneseSgQuestao = new ModeloAnamneseSgQuestao
            {
                ModeloAnamneseGid = modeloAnamneseGID,
                ModeloAnamneseSgid = modeloAnamneseSgID,
                Titulo = titulo,
                TipoOpcao = tipoOpcao,
                Ordem = ordem,
                Ativo = ativo
            };
            return modeloAnamneseSgQuestao;
        }

        public ModeloAnamneseSgQuestao AtualizarPropriedades(int modeloAnamneseGID, int modeloAnamneseSgID, string titulo, short tipoOpcao, short ordem, bool? ativo)
        {
            ModeloAnamneseGid = modeloAnamneseGID;
            ModeloAnamneseSgid = modeloAnamneseSgID;
            Titulo = titulo;
            TipoOpcao = tipoOpcao;
            Ordem = ordem;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
