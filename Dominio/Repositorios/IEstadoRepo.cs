using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IEstadoRepo : IBaseRepositorio<Estado>
    {
        Task<List<Estado>> BuscarPorPaisID(int paisID);
        Task<List<Estado>> BuscarPorNome(int paisID, string nome);
        Task<Estado>? BuscarPorUF(int paisID, string UF);
    }
}
