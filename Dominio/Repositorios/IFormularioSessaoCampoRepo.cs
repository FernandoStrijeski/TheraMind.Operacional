using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.FormulariosSessaoCampos
{
    public interface IFormularioSessaoCampoRepo : IBaseRepositorio<FormularioSessaoCampo, int>
    {
        Task<List<FormularioSessaoCampo>> BuscarFiltros(
            Expression<Func<FormularioSessaoCampo, bool>> filtro = null,
            Func<IQueryable<FormularioSessaoCampo>, IOrderedQueryable<FormularioSessaoCampo>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<FormularioSessaoCampo>? BuscarPorID(int formularioSessaoCampoID);
    }
}
