using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class AgendaSessaoItemViewModel
    {
        public int AgendaSessaoItemId { get; set; }
        public Guid EmpresaId { get; set; }
        public int FilialId { get; set; }
        public Guid ProfissionalId { get; set; }
        public int AgendaProfissionalId { get; set; }
        public int ServicoId { get; set; }
        public int FormularioSessaoId { get; set; }
        public int FormularioSessaoCampoId { get; set; }
        public FormularioSessaoCampoViewModel FormularioSessaoCampo { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid AgendaSessaoId { get; set; }
        public short CampoTipo { get; set; }
        public string? ConteudoTexto { get; set; }
        public byte[]? ConteudoArquivo { get; set; }
        public bool? Ativo { get; set; }      
    }
}
