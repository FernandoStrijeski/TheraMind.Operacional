using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.ModelosAnamneseG
{
    public interface IModeloAnamneseGRepo : IBaseRepositorio<ModeloAnamneseG, int>
    {
        Task<List<ModeloAnamneseG>> BuscarFiltros(
            Expression<Func<ModeloAnamneseG, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseG>, IOrderedQueryable<ModeloAnamneseG>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<ModeloAnamneseG>? BuscarPorID(int ModeloAnamneseGID);
    }
}
