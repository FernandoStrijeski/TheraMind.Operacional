using Dominio.Entidades;

namespace API.Operacional.modelos.ViewModels
{
    public class EmpresaFaturaIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da fatura da empresa criada
        /// </summary>
        public int EmpresaFaturaId { get; set; }

        public EmpresaFaturaIdResponseViewModel(int id)
        {
            EmpresaFaturaId = id;
        }
    }
}
