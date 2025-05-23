using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AgendaSessao
    {
        public AgendaSessao()
        {
            AgendaSessaoItens = new HashSet<AgendaSessaoItem>();
        }

        [Key]
        public Guid AgendaSessaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaProfissionalId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public Guid? ClienteId { get; set; }
        public short TipoEvento { get; set; }
        public short Modalidade { get; set; }
        public string? SalaId { get; set; }
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public bool? DiaTodo { get; set; }
        public short TipoRecorrencia { get; set; }
        public DateTime? RecorrenciaDataTermino { get; set; }
        public short? RecorrenciaNroSessoes { get; set; }
        public short Situacao { get; set; }
        public bool? PagamentoEfetuado { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public string? MotivoCancelamento { get; set; }
        public bool? MantemCobranca { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual AgendaProfissional Agenda { get; set; } = null!;
        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual FormularioSessao FormularioSessao { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual Sala? Sala { get; set; }
        public virtual Servico Servico { get; set; } = null!;
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItens { get; set; }


        public static AgendaSessao CriarParaImportacao(Guid empresaId, int filialId, Guid profissionalId, int agendaProfissionalId, int servicoId, int formularioSessaoId,
                                                       Guid? clienteId, short tipoEvento, short modalidade, string? salaId, DateTime dataHoraInicio, DateTime dataHoraFim,
                                                       bool? diaTodo, short tipoRecorrencia, DateTime? recorrenciaDataTermino, short? recorrenciaNroSessoes, short situacao,
                                                       bool? pagamentoEfetuado, DateTime? dataCancelamento, string? motivoCancelamento, bool? mantemCobranca)
        {
            var agendaSessao = new AgendaSessao
            {
                EmpresaId = empresaId,
                FilialId = filialId,
                ProfissionalId = profissionalId,
                AgendaProfissionalId = agendaProfissionalId,
                ServicoId = servicoId,
                FormularioSessaoId = formularioSessaoId,
                ClienteId = clienteId,
                TipoEvento = tipoEvento,
                Modalidade = modalidade,
                SalaId = salaId,
                DataHoraInicio = dataHoraInicio,
                DataHoraFim = dataHoraFim,
                DiaTodo = diaTodo,
                TipoRecorrencia = tipoRecorrencia,
                RecorrenciaDataTermino = recorrenciaDataTermino,
                RecorrenciaNroSessoes = recorrenciaNroSessoes,
                Situacao = situacao,
                PagamentoEfetuado = pagamentoEfetuado,
                DataCancelamento = dataCancelamento,
                MotivoCancelamento = motivoCancelamento,
                MantemCobranca = mantemCobranca
            };
            return agendaSessao;
        }

        public AgendaSessao AtualizarPropriedades(Guid empresaId, int filialId, Guid profissionalId, int agendaProfissionalId, int servicoId, int formularioSessaoId,
                                                 Guid? clienteId, short tipoEvento, short modalidade, string? salaId, DateTime dataHoraInicio, DateTime dataHoraFim,
                                                 bool? diaTodo, short tipoRecorrencia, DateTime? recorrenciaDataTermino, short? recorrenciaNroSessoes, short situacao,
                                                 bool? pagamentoEfetuado, DateTime? dataCancelamento, string? motivoCancelamento, bool? mantemCobranca)
        {
            EmpresaId = empresaId;
            FilialId = filialId;
            ProfissionalId = profissionalId;
            AgendaProfissionalId = agendaProfissionalId;
            ServicoId = servicoId;
            FormularioSessaoId = formularioSessaoId;
            ClienteId = clienteId;
            TipoEvento = tipoEvento;
            Modalidade = modalidade;

            if (salaId != null)
                SalaId = salaId;

            DataHoraInicio = dataHoraInicio;
            DataHoraFim = dataHoraFim;

            if (diaTodo != null)
                DiaTodo = diaTodo;

            TipoRecorrencia = tipoRecorrencia;

            if (RecorrenciaDataTermino != null)
                RecorrenciaDataTermino = recorrenciaDataTermino;

            if (recorrenciaNroSessoes != null)
                RecorrenciaNroSessoes = recorrenciaNroSessoes;

            Situacao = situacao;

            if (pagamentoEfetuado != null)
                PagamentoEfetuado = pagamentoEfetuado;

            if (dataCancelamento != null)
                DataCancelamento = dataCancelamento;

            if (motivoCancelamento != null)
                MotivoCancelamento = motivoCancelamento;

            if (mantemCobranca != null)
                MantemCobranca = mantemCobranca;

            return this;
        }
    }
}
