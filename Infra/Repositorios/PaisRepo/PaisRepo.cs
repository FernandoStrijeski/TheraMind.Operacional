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
    public class PaisRepo : BaseRepositorio<Pais>, IPaisRepo
    {
        public PaisRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Pais>> BuscarPorNome(string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Nome.Contains(nome)
                        
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Pais>? BuscarPorID(int paisID)
        {
            var query = _dbSet.AsQueryable();
            var pais = await query.FirstOrDefaultAsync(where => where.PaisId == paisID);
            return pais;
        }
    }
}
