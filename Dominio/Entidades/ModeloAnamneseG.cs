using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseG
    {
        public ModeloAnamneseG()
        {
            ModeloAnamneseSubGrupoQuestaoOpcoes = new HashSet<ModeloAnamneseSgQuestaoO>();
            ModeloAnamneseSubGrupoQuestoes = new HashSet<ModeloAnamneseSgQuestao>();
            ModeloAnamneseSubGrupos = new HashSet<ModeloAnamneseSg>();
        }

        [Key]
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<ModeloAnamneseSgQuestaoO> ModeloAnamneseSubGrupoQuestaoOpcoes { get; set; }
        public virtual ICollection<ModeloAnamneseSgQuestao> ModeloAnamneseSubGrupoQuestoes { get; set; }
        public virtual ICollection<ModeloAnamneseSg> ModeloAnamneseSubGrupos { get; set; }

        public static ModeloAnamneseG CriarParaImportacao(string titulo, bool? privado, bool editadoPorTodos, bool? ativo)
        {
            var modeloAnamneseG = new ModeloAnamneseG
            {
                Titulo = titulo,
                Privado = privado,
                EditadoPorTodos = editadoPorTodos,
                Ativo = ativo
            };
            return modeloAnamneseG;
        }

        public ModeloAnamneseG AtualizarPropriedades(string titulo, bool? privado, bool editadoPorTodos, bool? ativo)
        {
            Titulo = titulo;
            
            if(privado != null)
            Privado = privado;

            EditadoPorTodos = editadoPorTodos;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
