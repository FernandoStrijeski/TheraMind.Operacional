using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;
using System.Linq.Expressions;
using System.Linq;

namespace Dominio.ModelosAnamneseSGQuestaoOpcoes
{
    public interface IModeloAnamneseSgQuestaoORepo : IBaseRepositorio<ModeloAnamneseSgQuestaoO>
    {
        Task<List<ModeloAnamneseSgQuestaoO>> BuscarFiltros(
            Expression<Func<ModeloAnamneseSgQuestaoO, bool>> filtro = null,
            Func<IQueryable<ModeloAnamneseSgQuestaoO>, IOrderedQueryable<ModeloAnamneseSgQuestaoO>> orderBy = null,
            int skip = 0,
            int take = 0
            );        
        Task<ModeloAnamneseSgQuestaoO>? BuscarPorID(int ModeloAnamneseSgQuestaoOID);
    }
}
