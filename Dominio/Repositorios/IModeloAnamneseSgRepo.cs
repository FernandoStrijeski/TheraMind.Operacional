using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.ModelosAnamneseSG
{
    public interface IModeloAnamneseSgRepo : IBaseRepositorio<ModeloAnamneseSg, int>
    {
        Task<List<ModeloAnamneseSg>> BuscarFiltros(
            Expression<Func<ModeloAnamneseSg, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseSg>, IOrderedQueryable<ModeloAnamneseSg>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<ModeloAnamneseSg>? BuscarPorID(int ModeloAnamneseSgID);
    }
}
