using Dominio.Core.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dominio.Entidades;

namespace Dominio.Repositorios
{
    public interface IOrientacaoSexualRepo : IBaseRepositorio<OrientacaoSexual>
    {
        Task<List<OrientacaoSexual>> BuscarPorNome(string nome);
        Task<OrientacaoSexual> BuscarPorID(int orientacaoSexualID);
    }
}
