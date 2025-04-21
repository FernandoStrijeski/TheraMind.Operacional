using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IEstadoCivilRepo : IBaseRepositorio<EstadoCivil>
    {
        Task<List<EstadoCivil>> BuscarPorNome(string nome);
        Task<EstadoCivil> BuscarPorID(String estadoCivilID);
    }
}
