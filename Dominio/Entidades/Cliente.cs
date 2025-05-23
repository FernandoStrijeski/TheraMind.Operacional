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
            AgendaSessaoItens = new HashSet<AgendaSessaoItem>();
            AgendaSessoes = new HashSet<AgendaSessao>();
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
        public virtual ICollection<AgendaSessaoItem> AgendaSessaoItens { get; set; }
        public virtual ICollection<AgendaSessao> AgendaSessoes { get; set; }


        public static Cliente CriarParaImportacao(Guid empresaId, int filialId, string nomeCompleto, string? nomeSocial, DateTime dataNascimento,
                                                  string cpf, string? rg, string email, string celular, string sexo, int? identidadeGeneroId,
                                                  int? orientacaoSexualId, string? estadoCivilId, int? escolaridadeId, int? tipoEtniaId, string? profissao, bool? deficiencia, bool? naturalizado,
                                                  DateTime? dataNaturalizacao, string? nomeResponsavel, string? celularResponsavel, string? emailResponsavel, string? cpfresponsavel, string? rgresponsavel,
                                                  DateTime? dataNascimentoResponsavel, string? parenteNome, string? parenteCelular, int? parenteGrauParentescoId, string? tipoLogradouroId,
                                                  string? endereco, int? numero, string? cep, string? complemento, string? bairro, int? cidadeId, string? uf, int? paisId,
                                                  int? nacionalidadeId, short? planoPagamento, int? convenioId, decimal? convenioValorRepasse, int? pacoteFechadoId, short? planoMensalInicio,
                                                  DateTime? planoMensalDataVencimento, short? pacoteFechadoNroSessoes, DateTime? pacoteFechadoDataInicio, DateTime? pacoteFechadoDataVencimento,
                                                  decimal? valorPagamento, short? formaPagamento, short situacao, string? motivoDesativacao, Guid? usuarioID)
        {
            var cliente = new Cliente
            {
                EmpresaId = empresaId,
                FilialId = filialId,
                NomeCompleto = nomeCompleto,
                NomeSocial = nomeSocial,
                DataNascimento = dataNascimento,
                Cpf = cpf,
                Rg = rg,
                Email = email,
                Celular = celular,
                Sexo = sexo,
                IdentidadeGeneroId = identidadeGeneroId,
                OrientacaoSexualId = orientacaoSexualId,
                EstadoCivilId = estadoCivilId,
                EscolaridadeId = escolaridadeId,
                TipoEtniaId = tipoEtniaId,
                Profissao = profissao,
                Deficiencia = deficiencia,
                Naturalizado = naturalizado,
                DataNaturalizacao = dataNaturalizacao,
                NomeResponsavel = nomeResponsavel,
                CelularResponsavel = celularResponsavel,
                EmailResponsavel = emailResponsavel,
                Cpfresponsavel = cpfresponsavel,
                Rgresponsavel = rgresponsavel,
                DataNascimentoResponsavel = dataNascimentoResponsavel,
                ParenteNome = parenteNome,
                ParenteCelular = parenteCelular,
                ParenteGrauParentescoId = parenteGrauParentescoId,
                TipoLogradouroId = tipoLogradouroId,
                Endereco = endereco,
                Numero = numero,
                Cep = cep,
                Complemento = complemento,
                Bairro = bairro,
                CidadeId = cidadeId,
                Uf = uf,
                PaisId = paisId,
                NacionalidadeId = nacionalidadeId,
                PlanoPagamento = planoPagamento,
                ConvenioId = convenioId,
                ConvenioValorRepasse = convenioValorRepasse,
                PacoteFechadoId = pacoteFechadoId,
                PlanoMensalInicio = planoMensalInicio,
                PlanoMensalDataVencimento = planoMensalDataVencimento,
                PacoteFechadoNroSessoes = pacoteFechadoNroSessoes,
                PacoteFechadoDataInicio = pacoteFechadoDataInicio,
                PacoteFechadoDataVencimento = pacoteFechadoDataVencimento,
                ValorPagamento = valorPagamento,
                FormaPagamento = formaPagamento,
                Situacao = situacao,
                MotivoDesativacao = motivoDesativacao,
                UsuarioID = usuarioID
            };
            return cliente;
        }

        public Cliente AtualizarPropriedades(Guid empresaId, int filialId, string nomeCompleto, string? nomeSocial, DateTime dataNascimento,
                                            string cpf, string? rg, string email, string celular, string sexo, int? identidadeGeneroId,
                                            int? orientacaoSexualId, string? estadoCivilId, int? escolaridadeId, int? tipoEtniaId, string? profissao, bool? deficiencia, bool? naturalizado,
                                            DateTime? dataNaturalizacao, string? nomeResponsavel, string? celularResponsavel, string? emailResponsavel, string? cpfresponsavel, string? rgresponsavel,
                                            DateTime? dataNascimentoResponsavel, string? parenteNome, string? parenteCelular, int? parenteGrauParentescoId, string? tipoLogradouroId,
                                            string? endereco, int? numero, string? cep, string? complemento, string? bairro, int? cidadeId, string? uf, int? paisId,
                                            int? nacionalidadeId, short? planoPagamento, int? convenioId, decimal? convenioValorRepasse, int? pacoteFechadoId, short? planoMensalInicio,
                                            DateTime? planoMensalDataVencimento, short? pacoteFechadoNroSessoes, DateTime? pacoteFechadoDataInicio, DateTime? pacoteFechadoDataVencimento,
                                            decimal? valorPagamento, short? formaPagamento, short situacao, string? motivoDesativacao, Guid? usuarioID)
        {
            EmpresaId = empresaId;
            FilialId = filialId;
            NomeCompleto = nomeCompleto;

            if (nomeSocial != null)
                NomeSocial = nomeSocial;

            DataNascimento = dataNascimento;
            Cpf = cpf;

            if (rg != null)
                Rg = rg;
            Email = email;
            Celular = celular;
            Sexo = sexo;

            if (identidadeGeneroId != null)
                IdentidadeGeneroId = identidadeGeneroId;

            if (orientacaoSexualId != null)
                OrientacaoSexualId = orientacaoSexualId;

            if (estadoCivilId != null)
                EstadoCivilId = estadoCivilId;

            if (escolaridadeId != null)
                EscolaridadeId = escolaridadeId;

            if (tipoEtniaId != null)
                TipoEtniaId = tipoEtniaId;

            if (profissao != null)
                Profissao = profissao;

            if (deficiencia != null)
                Deficiencia = deficiencia;

            if (naturalizado != null)
                Naturalizado = naturalizado;

            if (dataNaturalizacao != null)
                DataNaturalizacao = dataNaturalizacao;

            if (nomeResponsavel != null)
                NomeResponsavel = nomeResponsavel;

            if (celularResponsavel != null)
                CelularResponsavel = celularResponsavel;

            if (emailResponsavel != null)
                EmailResponsavel = emailResponsavel;

            if (cpfresponsavel != null)
                Cpfresponsavel = cpfresponsavel;

            if (rgresponsavel != null)
                Rgresponsavel = rgresponsavel;

            if (dataNascimentoResponsavel != null)
                DataNascimentoResponsavel = dataNascimentoResponsavel;

            if (parenteNome != null)
                ParenteNome = parenteNome;

            if (parenteCelular != null)
                ParenteCelular = parenteCelular;

            if (parenteGrauParentescoId != null)
                ParenteGrauParentescoId = parenteGrauParentescoId;

            if (tipoLogradouroId != null)
                TipoLogradouroId = tipoLogradouroId;

            if (endereco != null)
                Endereco = endereco;

            if (numero != null)
                Numero = numero;

            if (cep != null)
                Cep = cep;

            if (complemento != null)
                Complemento = complemento;

            if (bairro != null)
                Bairro = bairro;

            if (cidadeId != null)
                CidadeId = cidadeId;

            if (uf != null)
                Uf = uf;

            if (paisId != null)
                PaisId = paisId;

            if (nacionalidadeId != null)
                NacionalidadeId = nacionalidadeId;

            if (planoPagamento != null)
                PlanoPagamento = planoPagamento;

            if (convenioId != null)
                ConvenioId = convenioId;

            if (convenioValorRepasse != null)
                ConvenioValorRepasse = convenioValorRepasse;

            if (pacoteFechadoId != null)
                PacoteFechadoId = pacoteFechadoId;

            if (planoMensalInicio != null)
                PlanoMensalInicio = planoMensalInicio;

            if (planoMensalDataVencimento != null)
                PlanoMensalDataVencimento = planoMensalDataVencimento;

            if (pacoteFechadoNroSessoes != null)
                PacoteFechadoNroSessoes = pacoteFechadoNroSessoes;

            if (pacoteFechadoDataInicio != null)
                PacoteFechadoDataInicio = pacoteFechadoDataInicio;

            if (pacoteFechadoDataVencimento != null)
                PacoteFechadoDataVencimento = pacoteFechadoDataVencimento;

            if (valorPagamento != null)
                ValorPagamento = valorPagamento;

            if (formaPagamento != null)
                FormaPagamento = formaPagamento;

            Situacao = situacao;

            if (motivoDesativacao != null)
                MotivoDesativacao = motivoDesativacao;

            if (usuarioID != null)
                UsuarioID = usuarioID;

            return this;
        }
    }
}
