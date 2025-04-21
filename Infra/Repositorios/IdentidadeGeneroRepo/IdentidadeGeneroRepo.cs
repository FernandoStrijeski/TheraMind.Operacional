using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Repositorios;
using Infra.Context;
using Infra.Core.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorios
{
    public class IdentidadeGeneroRepo : BaseRepositorio<IdentidadeGenero>, IIdentidadeGeneroRepo
    {
        public IdentidadeGeneroRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<IdentidadeGenero>? BuscarPorID(int identidadeGeneroId)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.IdentidadeGeneroId == identidadeGeneroId);
            return retorno;
        }

        public async Task<List<IdentidadeGenero>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.Contains(nome)
            ).AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
