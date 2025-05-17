using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public partial class Cliente
    {
        public Cliente()
        {
            AcompanhamentoClinicos = new HashSet<AcompanhamentoClinico>();
            AgendaSessaoItems = new HashSet<AgendaSessaoItem>();
            AgendaSessaos = new HashSet<AgendaSessao>();
        }

        [Key]
        public Guid ClienteId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public string NomeCompleto { get; set; } = null!;
        public string? NomeSocial { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; } = null!;
        public string? Rg { get; set; }
        public string Email { get; set; } = null!;
        public string Celular { get; set; } = null!;
        public string Sexo { get; set; } = null!;
        public int? IdentidadeGeneroId { get; set; }
        public int? OrientacaoSexualId { get; set; }
        public string? EstadoCivilId { get; set; }
        public int? EscolaridadeId { get; set; }
        public int? TipoEtniaId { get; set; }
        public string? Profissao { get; set; }
        public bool? Deficiencia { get; set; }
        public bool? Naturalizado { get; set; }
        public DateTime? DataNaturalizacao { get; set; }
        public string? NomeResponsavel { get; set; }
        public string? CelularResponsavel { get; set; }
        public string? EmailResponsavel { get; set; }
        public string? Cpfresponsavel { get; set; }
        public string? Rgresponsavel { get; set; }
        public DateTime? DataNascimentoResponsavel { get; set; }
        public string? ParenteNome { get; set; }
        public string? ParenteCelular { get; set; }
        public int? ParenteGrauParentescoId { get; set; }
        public string? TipoLogradouroId { get; set; }
        public string? Endereco { get; set; }
        public int? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public int? CidadeId { get; set; }
        public string? Uf { get; set; }
        public int? PaisId { get; set; }
        public int? NacionalidadeId { get; set; }
        public short? PlanoPagamento { get; set; }
        public int? ConvenioId { get; set; }
        public decimal? ConvenioValorRepasse { get; set; }
        public int? PacoteFechadoId { get; set; }
        public short? PlanoMensalInicio { get; set; }
        public DateTime? PlanoMensalDataVencimento { get; set; }
        public short? PacoteFechadoNroSessoes { get; set; }
        public DateTime? PacoteFechadoDataInicio { get; set; }
        public DateTime? PacoteFechadoDataVencimento { get; set; }
        public decimal? ValorPagamento { get; set; }
        public short? FormaPagamento { get; set; }
        public short Situacao { get; set; }
        public string? MotivoDesativacao { get; set; }
        public DateTime? DataCriacao { get; set; }
        public Guid? UsuarioID { get; set; }

        public virtual Cidade? Cidade { get; set; }
        public virtual Convenio? Convenio { get; set; }
        public virtual Empresa Empresa { get; set; } = null!;
        public virtual Escolaridade? Escolaridade { get; set; }
        public virtual EstadoCivil? EstadoCivil { get; set; }
        public virtual Filial Filial { get; set; } = null!;
        public virtual IdentidadeGenero? IdentidadeGenero { get; set; }
        public virtual Nacionalidade? Nacionalidade { get; set; }
        public virtual OrientacaoSexual? OrientacaoSexual { get; set; }
        public virtual PacoteFechado? PacoteFechado { get; set; }
        public virtual Pais? Pais { get; set; }
        public virtual GrauParentesco? ParenteGrauParentesco { get; set; }
        public virtual TipoEtnia? TipoEtnia { get; set; }
        public virtual TipoLogradouro? TipoLogradouro { get; set; }
        public virtual Estado? UfNavigation { get; set; }
        public virtual ICollection<AcompanhamentoClinico> AcompanhamentoClinicos { get; set; }
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItems { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessaos { get; set; }
    }
}
