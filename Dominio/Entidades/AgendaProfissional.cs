using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AgendaProfissional
    {
        public AgendaProfissional()
        {
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
            AgendaSessaos = new HashSet<AgendaSessao>();
        }

        [Key]
        public int AgendaProfissionalId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int ExibicaoEmMinutos { get; set; }
        public int DuracaoSessaoMinutos { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public string? DiasOcultados { get; set; }
        public bool? ExibeSessoesAusentesCanc { get; set; }
        public bool? ExibeComparecimento { get; set; }
        public bool? ExibePagamento { get; set; }
        public bool? ExibeFeriadosNacionais { get; set; }
        public short TipoVisualizacao { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }

        public static AgendaProfissional CriarParaImportacao(Guid empresaID, int filialID, Guid profissionalID, int exibicaoEmMinutos, int duracaoSessaoMinutos, TimeSpan horaInicio, TimeSpan horaFim, 
                                                            string? diasOcultados, bool? exibeSessoesAusentesCanc, bool? exibeComparecimento, bool? exibePagamento, bool? exibeFeriadosNacionais, short tipoVisualizacao)
        {
            var agendaProfissional = new AgendaProfissional
            {
                EmpresaId = empresaID,
                FilialId = filialID,
                ProfissionalId = profissionalID,
                ExibicaoEmMinutos = exibicaoEmMinutos,
                DuracaoSessaoMinutos = duracaoSessaoMinutos,
                HoraInicio = horaInicio,
                HoraFim = horaFim,
                DiasOcultados = diasOcultados,
                ExibeSessoesAusentesCanc = exibeSessoesAusentesCanc,
                ExibeComparecimento = exibeComparecimento,
                ExibePagamento = exibePagamento,
                ExibeFeriadosNacionais = exibeFeriadosNacionais,
                TipoVisualizacao = tipoVisualizacao
            };
            return agendaProfissional;
        }

        public AgendaProfissional AtualizarPropriedades(Guid empresaID, int filialID, Guid profissionalID, int exibicaoEmMinutos, int duracaoSessaoMinutos, TimeSpan horaInicio, TimeSpan horaFim,
                                                            string? diasOcultados, bool? exibeSessoesAusentesCanc, bool? exibeComparecimento, bool? exibePagamento, bool? exibeFeriadosNacionais, short tipoVisualizacao)
        {
            EmpresaId = empresaID;
            FilialId = filialID;
            ProfissionalId = profissionalID;
            ExibicaoEmMinutos = exibicaoEmMinutos;
            DuracaoSessaoMinutos = duracaoSessaoMinutos;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            DiasOcultados = diasOcultados;

            if(exibeSessoesAusentesCanc != null)
                ExibeSessoesAusentesCanc = exibeSessoesAusentesCanc;

            if (exibeComparecimento != null)
                ExibeComparecimento = exibeComparecimento;

            if (exibePagamento != null)
                ExibePagamento = exibePagamento;

            if (exibeFeriadosNacionais != null)
                ExibeFeriadosNacionais = exibeFeriadosNacionais;
            
            TipoVisualizacao = tipoVisualizacao;

            return this;
        }
    }
}
