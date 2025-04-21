using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IPaisRepo : IBaseRepositorio<Pais>
    {
        Task<List<Pais>> BuscarPorNome(string nome);
        Task<Pais>? BuscarPorID(int paisID);
    }
}
