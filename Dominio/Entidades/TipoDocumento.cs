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
    }
}
