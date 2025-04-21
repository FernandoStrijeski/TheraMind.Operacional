using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IEscolaridadeRepo : IBaseRepositorio<Escolaridade>
    {
        Task<List<Escolaridade>> BuscarPorNome(string nome);
        Task<Escolaridade>? BuscarPorID(int escolaridadeID);
    }
}
