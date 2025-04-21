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
    public class CidadeRepo : BaseRepositorio<Cidade>, ICidadeRepo
    {
        public CidadeRepo(ApplicationDbContext contexto) : base(contexto) { }

        public async Task<List<Cidade>> BuscarPorPaisID(int paisID)
        {
            var query = _dbSet.Where(
                tabela => tabela.PaisId == paisID
            ).AsNoTracking();

            return await query.ToListAsync();

        }
        public async Task<List<Cidade>> BuscarPorNome(int paisID, string UF, string nome)
        {
            var query = _dbSet.Where(
                tabela => tabela.PaisId == paisID && tabela.Uf == UF && tabela.Nome.ToUpper().Contains(nome.ToUpper())                        
            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<List<Cidade>> BuscarPorUF(int paisID, string UF)
        {
            var query = _dbSet.Where(
                tabela => tabela.PaisId == paisID && tabela.Uf == UF

            ).AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Cidade>? BuscarPorID(int paisID, string UF, int cidadeID)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.PaisId == paisID && where.Uf == UF && where.CidadeId == cidadeID);
            return retorno;
        }
        public async Task<Cidade>? BuscarPorCodigoIBGE(int paisID, string UF, int codigoIBGE)
        {
            var query = _dbSet.AsQueryable();
            var retorno = await query.FirstOrDefaultAsync(where => where.PaisId == paisID && where.Uf == UF && where.CodigoIbge == codigoIBGE);
            return retorno;
        }
    }
}
