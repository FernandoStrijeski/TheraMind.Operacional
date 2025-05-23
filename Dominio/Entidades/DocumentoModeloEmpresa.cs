using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class DocumentoModeloEmpresa
    {
        [Key]
        public int DocumentoModeloEmpresaID { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short ConteudoTipo { get; set; }
        public string Conteudo { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Filial Filial { get; set; } = null!;
        public virtual TipoDocumento TipoDocumento { get; set; } = null!;

        public static DocumentoModeloEmpresa CriarParaImportacao(Guid empresaId, int filialId, int tipoDocumentoId, string titulo, short conteudoTipo, string conteudo, bool? ativo)
        {
            var convenio = new DocumentoModeloEmpresa
            {
                EmpresaId = empresaId,
                FilialId = filialId,
                TipoDocumentoId = tipoDocumentoId,
                Titulo = titulo,
                ConteudoTipo = conteudoTipo,
                Conteudo = conteudo,
                Ativo = ativo
            };
            return convenio;
        }

        public DocumentoModeloEmpresa AtualizarPropriedades(Guid empresaId, int filialId, int tipoDocumentoId, string titulo, short conteudoTipo, string conteudo, bool? ativo)
        {
            EmpresaId = empresaId;
            FilialId = filialId;
            TipoDocumentoId = tipoDocumentoId;
            Titulo = titulo;
            ConteudoTipo = conteudoTipo;
            Conteudo = conteudo;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
