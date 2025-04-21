using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface ICidadeRepo : IBaseRepositorio<Cidade>
    {
        Task<List<Cidade>> BuscarPorPaisID(int paisID);
        Task<List<Cidade>> BuscarPorUF(int paisID, string UF);
        Task<List<Cidade>> BuscarPorNome(int paisID, string UF, string nome);
        Task<Cidade>? BuscarPorID(int paisID, string UF, int CidadeID);
        Task<Cidade>? BuscarPorCodigoIBGE(int paisID, string UF, int CodigoIBGE);
    }
}
