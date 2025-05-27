using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.EmpresaFaturas
{
    public interface IEmpresaFaturaRepo : IBaseRepositorio<EmpresaFatura>
    {
        Task<List<EmpresaFatura>> BuscarFiltros(
            Expression<Func<EmpresaFatura, bool>> filtro = null,
            Func<IQueryable<EmpresaFatura>, IOrderedQueryable<EmpresaFatura>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<EmpresaFatura>? BuscarPorID(int empresaFaturaID);
    }
}
