using Dominio.Entidades;
using Xunit;

namespace tests.Dominio.tests
{
    public class PrazosContratosTests
    {
        public PrazosContratosTests() { }

        [Fact]
        public void PrazosContratos_Criar()
        {
            var prazosContratos = "1-060030;2-045015;3-145045;";

            var prazos = new PrazosContratos(prazosContratos);
            Assert.Equal(3, prazos.Prazos.Count);
            Assert.Equal("1", prazos.Prazos[0].Codigo);
            Assert.Equal("060", prazos.Prazos[0].PrimeiroPrazo);
            Assert.Equal("030", prazos.Prazos[0].SegundoPrazo);
            Assert.Equal("2", prazos.Prazos[1].Codigo);
            Assert.Equal("045", prazos.Prazos[1].PrimeiroPrazo);
            Assert.Equal("015", prazos.Prazos[1].SegundoPrazo);
            Assert.Equal("3", prazos.Prazos[2].Codigo);
            Assert.Equal("145", prazos.Prazos[2].PrimeiroPrazo);
            Assert.Equal("045", prazos.Prazos[2].SegundoPrazo);
        }
    }
}
