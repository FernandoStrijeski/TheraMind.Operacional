using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class AgendaSessaoItem
    {
        [Key]
        public int AgendaSessaoItemId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaProfissionalId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public int FormularioSessaoCampoId { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid AgendaSessaoId { get; set; }
        public short CampoTipo { get; set; }
        public string? ConteudoTexto { get; set; }
        public byte[]? ConteudoArquivo { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual AgendaProfissional Agenda { get; set; } = null!;
        public virtual AgendaSessao AgendaSessao { get; set; } = null!;
        public virtual Cliente? Cliente { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual FormularioSessao FormularioSessao { get; set; } = null!;
        public virtual FormularioSessaoCampo FormularioSessaoCampo { get; set; } = null!;
        public virtual Profissional Profissional { get; set; } = null!;
        public virtual Servico Servico { get; set; } = null!;

        public static AgendaSessaoItem CriarParaImportacao(Guid empresaId, int filialId, Guid profissionalId, int agendaProfissionalId, int servicoId, int formularioSessaoId, int formularioSessaoCampoId,
                                                           Guid? clienteId, Guid agendaSessaoId, short campoTipo, string? conteudoTexto, byte[]? conteudoArquivo, bool? ativo)
        {
            var agendaSessaoItem = new AgendaSessaoItem
            {
                EmpresaId = empresaId,
                FilialId = filialId,
                ProfissionalId = profissionalId,
                AgendaProfissionalId = agendaProfissionalId,
                ServicoId = servicoId,
                FormularioSessaoId = formularioSessaoId,
                FormularioSessaoCampoId = formularioSessaoCampoId,
                ClienteId = clienteId,
                AgendaSessaoId = agendaSessaoId,
                CampoTipo = campoTipo,
                ConteudoTexto = conteudoTexto,
                ConteudoArquivo = conteudoArquivo,
                Ativo = ativo
            };
            return agendaSessaoItem;
        }

        public AgendaSessaoItem AtualizarPropriedades(Guid empresaId, int filialId, Guid profissionalId, int agendaProfissionalId, int servicoId, int formularioSessaoId, int formularioSessaoCampoId,
                                                       Guid? clienteId, Guid agendaSessaoId, short campoTipo, string? conteudoTexto, byte[]? conteudoArquivo, bool? ativo)
        {
            EmpresaId = empresaId;
            FilialId = filialId;
            ProfissionalId = profissionalId;
            AgendaProfissionalId = agendaProfissionalId;
            ServicoId = servicoId;
            FormularioSessaoId = formularioSessaoId;
            FormularioSessaoCampoId = formularioSessaoCampoId;
            ClienteId = clienteId;
            AgendaSessaoId = agendaSessaoId;
            CampoTipo = campoTipo;

            if (conteudoTexto != null)
                ConteudoTexto = conteudoTexto;

            if (conteudoArquivo != null)
                ConteudoArquivo = conteudoArquivo;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
