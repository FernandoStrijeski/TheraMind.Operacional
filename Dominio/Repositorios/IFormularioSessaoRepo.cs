using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.FormulariosSessoes
{
    public interface IFormularioSessaoRepo : IBaseRepositorio<FormularioSessao>
    {
        Task<List<FormularioSessao>> BuscarFiltros(
            Expression<Func<FormularioSessao, bool>> filtro = null,
            Func<IQueryable<FormularioSessao>, IOrderedQueryable<FormularioSessao>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<FormularioSessao>? BuscarPorID(int formularioSessaoID);
    }
}
