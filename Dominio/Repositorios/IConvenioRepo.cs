using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.Convenios
{
    public interface IConvenioRepo : IBaseRepositorio<Convenio, int>
    {
        Task<List<Convenio>> BuscarFiltros(
            Expression<Func<Convenio, bool>> filtro = null,
            Func<IQueryable<Convenio>, IOrderedQueryable<Convenio>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<Convenio>? BuscarPorID(int empresaFaturaID);
    }
}
