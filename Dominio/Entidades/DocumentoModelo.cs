using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class DocumentoModelo
    {
        [Key]
        public int DocumentoModeloId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string Titulo { get; set; } = null!;
        public short ConteudoTipo { get; set; }
        public string ConteudoTexto { get; set; } = null!;
        public byte[] ConteudoArquivo { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; } = null!;
        public static DocumentoModelo CriarParaImportacao(int tipoDocumentoId, string titulo, short conteudoTipo, string conteudoTexto, byte[] conteudoArquivo, bool? ativo)
        {
            var convenio = new DocumentoModelo
            {
                TipoDocumentoId = tipoDocumentoId,
                Titulo = titulo,
                ConteudoTipo = conteudoTipo,
                ConteudoTexto = conteudoTexto,
                ConteudoArquivo = conteudoArquivo,
                Ativo = ativo
            };
            return convenio;
        }

        public DocumentoModelo AtualizarPropriedades(int tipoDocumentoId, string titulo, short conteudoTipo, string conteudoTexto, byte[] conteudoArquivo, bool? ativo)
        {
            TipoDocumentoId = tipoDocumentoId;
            Titulo = titulo;
            ConteudoTipo = conteudoTipo;
            ConteudoTexto = conteudoTexto;
            ConteudoArquivo = conteudoArquivo;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
