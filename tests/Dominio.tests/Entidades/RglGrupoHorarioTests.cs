using Dominio.Entidades;
using Xunit;
using Faker;

namespace tests.Dominio.tests.Entidades
{
    public class RglGrupoHorarioTests
    {
        public RglGrupoHorarioTests() { }

        [Fact]
        public void CriarParaImportacao_Cria_Novo_RglGrupoHorario()
        {
            var codEmpresa = "123";
            var entidade = RglGrupoHorario.CriarParaImportacao(codEmpresa);
            Assert.Equal(entidade.CodEmpresa, codEmpresa);
            Assert.Equal(entidade.CodGrupo, "G001");
            Assert.Equal(entidade.Descricao, "Grupo 001");
            Assert.Equal(entidade.Chmensal, 220);
            Assert.Equal(entidade.Chsemanal, (decimal)44.7);
            Assert.Equal(entidade.Chdiaria, (decimal)7.3333);
            AssertValoresPadraoNulosNaCriacao(entidade);
        }

        [Fact]
        public void Atualizar_Atualiza_Valores()
        {
            string descricao = Faker.Lorem.Sentence();
            decimal chmensal = Faker.RandomNumber.Next(1, 1000);
            decimal chsemanal = Faker.RandomNumber.Next(1, 1000);
            decimal chdiaria = Faker.RandomNumber.Next(1, 1000);
            short diasValeTransporte = (short)Faker.RandomNumber.Next(1, 7);
            short diasValeRefeicao = (short)Faker.RandomNumber.Next(1, 20);
            short indGrpHorPrincipal = (short)Faker.RandomNumber.Next(0, 1);
            string codGrpHorPrincipal = Faker.RandomNumber.Next(1, 1000).ToString();
            decimal chtimeSheet = Faker.RandomNumber.Next(1, 1000);
            short habilitado = (short)Faker.RandomNumber.Next(0, 1);
            var entidade = RglGrupoHorario.CriarParaImportacao("123");

            AssertValoresPadraoNulosNaCriacao(entidade);

            entidade.Atualizar(
                descricao,
                chmensal,
                chsemanal,
                chdiaria,
                diasValeTransporte,
                diasValeRefeicao,
                indGrpHorPrincipal,
                codGrpHorPrincipal,
                chtimeSheet,
                habilitado
            );
        }

        private void AssertValoresPadraoNulosNaCriacao(RglGrupoHorario entidade)
        {
            Assert.Equal(entidade.DiasValeTransporte, null);
            Assert.Equal(entidade.DiasValeRefeicao, null);
            Assert.Null(entidade.IndGrpHorPrincipal);
            Assert.Null(entidade.CodGrpHorPrincipal);
            Assert.Null(entidade.ChtimeSheet);
            Assert.Null(entidade.Habilitado);
        }
    }
}
