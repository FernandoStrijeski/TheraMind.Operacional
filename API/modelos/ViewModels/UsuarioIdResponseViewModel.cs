namespace API.Operacional.modelos.ViewModels
{
    public class UsuarioIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do usuário criado
        /// </summary>
        public Guid UsuarioId { get; set; }

        public UsuarioIdResponseViewModel(Guid id)
        {
            UsuarioId = id;
        }
    }
}
