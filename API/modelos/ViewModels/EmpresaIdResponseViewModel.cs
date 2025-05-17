namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class EmpresaIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da empresa criada
        /// </summary>
        public Guid EmpresaId { get; set; }

        public EmpresaIdResponseViewModel(Guid id)
        {
            EmpresaId = id;
        }
    }
}
