using Dominio.Entidades;
using Xunit;

namespace tests.Dominio.tests.Entidades
{
    public class ControleGlobalTests
    {
        public ControleGlobalTests() { }

        [Fact]
        public void ControleGlobal_Deve_Ser_Criado_Com_Sucesso_Sem_Valor_de_CtlUltColab()
        {
            var controleGlobal = new ControleGlobal();
            Assert.NotNull(controleGlobal);
            Assert.True(controleGlobal.CtlUltColab == 0);
        }

        [Fact]
        public void ControleGlobal_CloneSuperior_Deveria_Criar_Clone_Com_Valor_de_CtlUltColab_Aumentado_Em_1()
        {
            var controleGlobal = new ControleGlobal();
            var controleGlobalClone = controleGlobal.CloneSuperior();
            Assert.NotNull(controleGlobalClone);
            Assert.True(controleGlobalClone.CtlUltColab == 1);
        }
    }
}
