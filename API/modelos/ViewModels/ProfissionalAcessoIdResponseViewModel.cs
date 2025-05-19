namespace API.Operacional.modelos.ViewModels
{
    public class ProfissionalAcessoIdResponseViewModel
    {
        /// <summary>
        /// Identificador único do acesso do profissional criado
        /// </summary>
        public int ProfissionalAcessoId { get; set; }

        public ProfissionalAcessoIdResponseViewModel(int id)
        {
            ProfissionalAcessoId = id;
        }
    }
}
