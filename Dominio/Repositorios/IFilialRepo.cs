using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IFilialRepo : IBaseRepositorio<Filial>
    {
        Task<Filial>? BuscarPorID(Guid empresaID, int filialID);
        Task<List<Filial>> BuscarTodasPorEmpresa(Guid empresaID);
    }
}
