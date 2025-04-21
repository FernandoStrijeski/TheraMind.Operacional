using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class ModeloAnamneseG
    {
        public ModeloAnamneseG()
        {
            ModeloAnamneseSgquestaoOs = new HashSet<ModeloAnamneseSgquestaoO>();
            ModeloAnamneseSgquestaos = new HashSet<ModeloAnamneseSgquestao>();
            ModeloAnamneseSgs = new HashSet<ModeloAnamneseSg>();
        }

        [Key]
        public int ModeloAnamneseGid { get; set; }
        public string Titulo { get; set; } = null!;
        public bool? Privado { get; set; }
        public bool EditadoPorTodos { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<ModeloAnamneseSgquestaoO> ModeloAnamneseSgquestaoOs { get; set; }
        public virtual ICollection<ModeloAnamneseSgquestao> ModeloAnamneseSgquestaos { get; set; }
        public virtual ICollection<ModeloAnamneseSg> ModeloAnamneseSgs { get; set; }
    }
}
