using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AcompanhamentoClinico
    {
        [Key]
        public Guid AcompanhamentoClinicoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public Guid? ClienteId { get; set; }
        public string? AvaliacaoDemanda { get; set; }
        public string? PlanoTratamento { get; set; }
        public string? Diagnostico { get; set; }
        public string? RegistroEncerramento { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;

        public static AcompanhamentoClinico CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, Guid? clienteID, string? avaliacaoDemanda, string? planoTratamento, string? diagnostico, string? registroEncerramento)
        {
            var convenio = new AcompanhamentoClinico
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ProfissionalId = profissionalID,
                ClienteId = clienteID,
                AvaliacaoDemanda = avaliacaoDemanda,
                PlanoTratamento = planoTratamento,
                Diagnostico = diagnostico,
                RegistroEncerramento = registroEncerramento
            };
            return convenio;
        }

        public AcompanhamentoClinico AtualizarPropriedades(Guid empresaID, int filialID, Guid profissionalID, Guid? clienteID, string? avaliacaoDemanda, string? planoTratamento, string? diagnostico, string? registroEncerramento)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ProfissionalId = profissionalID;

            if (clienteID != null)
                ClienteId = clienteID;

            if (avaliacaoDemanda != null)
                AvaliacaoDemanda = avaliacaoDemanda;

            if (planoTratamento != null)
                PlanoTratamento = planoTratamento;

            if (diagnostico != null)
                Diagnostico = diagnostico;

            if (registroEncerramento != null)
                RegistroEncerramento = registroEncerramento;

            return this;
        }
    }
}
