using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IIdentidadeGeneroRepo : IBaseRepositorio<IdentidadeGenero>
    {
        Task<List<IdentidadeGenero>> BuscarPorNome(string nome);
        Task<IdentidadeGenero> BuscarPorID(int identidadeGeneroID);
    }
}
