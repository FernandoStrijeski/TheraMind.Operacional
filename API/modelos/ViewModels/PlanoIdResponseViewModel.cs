namespace API.Operacional.modelos.ViewModels
{
    public class PlanoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do plano criado
        /// </summary>
        public Guid PlanoId { get; set; }

        public PlanoIdResponseViewModel(Guid id)
        {
            PlanoId = id;
        }
    }
}
