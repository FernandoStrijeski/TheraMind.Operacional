using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Documento
    {
        [Key]  // Define a chave primária
        public int DocumentoId { get; set; }
        public int CandidatoId { get; set; }
        public int TipoDocumentoId { get; set; }
        public string? Dados { get; set; }
        public byte[]? AnexoFrente { get; set; }
        public byte[]? AnexoVerso { get; set; }
        public DateTime? DataCriacao { get; set; }

        public TipoDocumento TipoDocumento { get; set; } = null!;
    }
}
