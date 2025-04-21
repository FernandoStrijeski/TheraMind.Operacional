using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface INacionalidadeRepo : IBaseRepositorio<Nacionalidade>
    {
        Task<List<Nacionalidade>> BuscarPorNome(string nome);
        Task<Nacionalidade>? BuscarPorID(int nacionalidadeID);
    }
}
