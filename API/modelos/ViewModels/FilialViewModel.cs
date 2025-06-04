using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class FilialViewModel
    {
        public int FilialId { get; set; }
        public Guid EmpresaId { get; set; }
        public string? Cpf { get; set; }
        public string? Cnpj { get; set; }
        public string? InscricaoEstadual { get; set; }
        public string? InscricaoMunicipal { get; set; }
        public string? NomeFilial { get; set; }
        public string? TipoLogradouroId { get; set; }
        public string? Endereco { get; set; }
        public short? Numero { get; set; }
        public string? Cep { get; set; }
        public string? Complemento { get; set; }
        public string? Bairro { get; set; }
        public int CidadeId { get; set; }
        public string? Telefone { get; set; }
        public bool? Ativo { get; set; }
        public DateTime? DataCriacao { get; set; }
        public virtual EmpresaViewModel Empresa { get; set; }
    }
}
