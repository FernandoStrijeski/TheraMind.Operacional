namespace API.Operacional.modelos.ViewModels
{
    public class ProfissionalIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do profissional criado
        /// </summary>
        public Guid ProfissionalId { get; set; }

        public ProfissionalIdResponseViewModel(Guid id)
        {
            ProfissionalId = id;
        }
    }
}
