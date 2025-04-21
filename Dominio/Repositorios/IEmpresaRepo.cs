using Dominio.Core.Repositorios;
using Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace Dominio.Repositorios
{
    public interface IEmpresaRepo : IBaseRepositorio<Empresa>
    {
        Task<Empresa>? BuscarPorID(Guid empresaID);
    }
}
