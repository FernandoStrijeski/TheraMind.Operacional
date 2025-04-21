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
    public class EscolaridadeRepo : BaseRepositorio<Escolaridade>, IEscolaridadeRepo
    {
        public EscolaridadeRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Escolaridade>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.ToUpper().Contains(nome.ToUpper())
                        
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Escolaridade>? BuscarPorID(int escolaridadeID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.EscolaridadeId == escolaridadeID);
            return retorno;
        }
    }
}
