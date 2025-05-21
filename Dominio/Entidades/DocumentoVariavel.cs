using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class DocumentoVariavel
    {
        [Key]
        public int DocumentoVariavelId { get; set; }
        public string NomeVariavel { get; set; } = null!;
        public string NomeCampo { get; set; } = null!;
        public string NomeTabela { get; set; } = null!;
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }

        public static DocumentoVariavel CriarParaImportacao(string nomeVariavel, string nomeCampo, string nomeTabela, bool? ativo)
        {
            var convenio = new DocumentoVariavel
            {
                NomeVariavel = nomeVariavel,
                NomeCampo = nomeCampo,
                NomeTabela = nomeTabela,
                Ativo = ativo
            };
            return convenio;
        }

        public DocumentoVariavel AtualizarPropriedades(string nomeVariavel, string nomeCampo, string nomeTabela, bool? ativo)
        {
            NomeVariavel = nomeVariavel;
            NomeCampo = nomeCampo;
            NomeTabela = nomeTabela;

            if (ativo != null)
                Ativo = ativo;

            return this;
        }
    }
}
