using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.ModelosAnamneseSGQuestoes
{
    public interface IModeloAnamneseSgQuestaoRepo : IBaseRepositorio<ModeloAnamneseSgQuestao>
    {
        Task<List<ModeloAnamneseSgQuestao>> BuscarFiltros(
            Expression<Func<ModeloAnamneseSgQuestao, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseSgQuestao>, IOrderedQueryable<ModeloAnamneseSgQuestao>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<ModeloAnamneseSgQuestao>? BuscarPorID(int ModeloAnamneseSgQuestaoID);
    }
}
