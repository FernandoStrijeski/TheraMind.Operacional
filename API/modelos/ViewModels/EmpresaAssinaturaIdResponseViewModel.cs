namespace API.AdmissaoDigital.modelos.ViewModels
{
    public class EmpresaAssinaturaIdResponseViewModel
    {
        /// <summary>
        /// Identificador único da assinatura da empresa criada
        /// </summary>
        public Guid EmpresaAssinaturaId { get; set; }

        public EmpresaAssinaturaIdResponseViewModel(Guid id)
        {
            EmpresaAssinaturaId = id;
        }
    }
}
