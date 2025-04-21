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
    public class NacionalidadeRepo : BaseRepositorio<Nacionalidade>, INacionalidadeRepo
    {
        public NacionalidadeRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<Nacionalidade>? BuscarPorID(int nacionalidadeID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.NacionalidadeId == nacionalidadeID);
            return retorno;
        }

        public async Task<List<Nacionalidade>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.Contains(nome)
            ).AsNoTracking();

            return await query.ToListAsync();
        }
    }
}
