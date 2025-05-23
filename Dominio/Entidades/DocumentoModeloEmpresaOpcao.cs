using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class DocumentoModeloEmpresaOpcao
    {
        [Key]
        public int DocumentoModeloEmpresaOpcaoId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public short TipoOpcao { get; set; }
        public string ConteudoBase64 { get; set; } = null!;
        public decimal? Transparencia { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public static DocumentoModeloEmpresaOpcao CriarParaImportacao(Guid empresaId, int filialId, short tipoOpcao, string conteudoBase64, decimal? transparencia, bool? ativo)
        {
            var convenio = new DocumentoModeloEmpresaOpcao
            {
                EmpresaId = empresaId,
                FilialId = filialId,
                TipoOpcao = tipoOpcao,
                ConteudoBase64 = conteudoBase64,
                Transparencia = transparencia,
                Ativo = ativo
            };
            return convenio;
        }

        public DocumentoModeloEmpresaOpcao AtualizarPropriedades(Guid empresaId, int filialId, short tipoOpcao, string conteudoBase64, decimal? transparencia, bool? ativo)
        {
            EmpresaId = empresaId;
            FilialId = filialId;
            TipoOpcao = tipoOpcao;
            ConteudoBase64 = conteudoBase64;

            if(transparencia != null)
                Transparencia = transparencia;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
