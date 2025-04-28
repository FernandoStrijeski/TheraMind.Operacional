using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            DocumentoModeloEmpresas = new HashSet<DocumentoModeloEmpresa>();
            DocumentoModelos = new HashSet<DocumentoModelo>();
        }

        [Key]
        public int TipoDocumentoId { get; set; }
        public string Descricao { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public virtual ICollection<DocumentoModeloEmpresa> DocumentoModeloEmpresas { get; set; }
        public virtual ICollection<DocumentoModelo> DocumentoModelos { get; set; }

        public static TipoDocumento CriarParaImportacao(string descricao, bool ativo)
        {
            var tipoDocumento = new TipoDocumento
            {
                Descricao = descricao,                
                Ativo = ativo
            };
            return tipoDocumento;
        }

        public TipoDocumento AtualizarPropriedades(
            string descricao,
            bool? ativo = true
        )
        {
            Descricao = descricao;            
            if (ativo != null)
                Ativo = ativo;
            return this;
        }
    }
}
