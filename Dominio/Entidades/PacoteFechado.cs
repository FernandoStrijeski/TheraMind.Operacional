using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class PacoteFechado
    {
        public PacoteFechado()
        {
            Clientes = new HashSet<Cliente>();
        }

        [Key]
        public int PacoteFechadoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int QuantidadeSessoes { get; set; }
        public decimal ValorTotal { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual ICollection<Cliente> Clientes { get; set; }


        public static PacoteFechado CriarParaImportacao(Guid empresaID, int filialID, int quantidadeSessoes, decimal valorTotal, bool? ativo)
        {
            var pacoteFechado = new PacoteFechado
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                QuantidadeSessoes = quantidadeSessoes,
                ValorTotal = valorTotal,
                Ativo = ativo
            };
            return pacoteFechado;
        }

        public PacoteFechado AtualizarPropriedades(Guid empresaID, int filialID, int quantidadeSessoes, decimal valorTotal, bool? ativo)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            QuantidadeSessoes = quantidadeSessoes;
            ValorTotal = valorTotal;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
