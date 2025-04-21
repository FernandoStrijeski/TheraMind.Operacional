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
    public class EstadoRepo : BaseRepositorio<Estado>, IEstadoRepo
    {
        public EstadoRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Estado>> BuscarPorPaisID(int paisID)
        {
            var query = _dbSet.Where(
                tabela => tabela.PaisId == paisID
            ).AsNoTracking();

            return await query.ToListAsync();

        }
        public async Task<List<Estado>> BuscarPorNome(int paisID, string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.Descricao.ToUpper().Contains(nome.ToUpper())
                        
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Estado>? BuscarPorUF(int paisID, string UF)
        {
            var query = _dbSet.AsQueryable();
            var pais = await query.FirstOrDefaultAsync(where => where.PaisId == paisID && where.Uf == UF);
            return pais;
        }
    }
}
