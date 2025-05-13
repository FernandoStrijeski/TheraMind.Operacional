using Dominio.Entidades;

namespace API.Servicos.Auditorias
{
    public interface IAuditoriaServico
    {
        /// <summary>
        /// Grava as informações na trilha de auditoria
        /// </summary>
        Task GravaTrilha(Auditoria auditoriaDTO);
    }
}
