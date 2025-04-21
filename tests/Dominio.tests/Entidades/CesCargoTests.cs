using Dominio.Entidades;
using Xunit;

namespace tests.Dominio.tests.Entidades
{
    public class CesCargoTests
    {
        public CesCargoTests() { }

        [Fact]
        public void Criar_Cria_Novo_Cargo()
        {
            var codEmp = "123";
            var codCarg = "456";
            var cesCargo = CesCargo.CriarParaImportacao(codEmp, codCarg);

            Assert.Equal(codEmp, cesCargo.CodEmpresa);
            Assert.Equal(codCarg, cesCargo.CodCargo);
            Assert.Equal("Gerado na Importação", cesCargo.Denominacao);
            Assert.Null(cesCargo.CodCbo);
            Assert.Null(cesCargo.CodCbo2002);
            Assert.Equal(cesCargo.Habilitado, (short)0);
            Assert.Null(cesCargo.DenomEstrangeira);
            Assert.Null(cesCargo.IdCotaMenorAprendiz);
            Assert.Null(cesCargo.IdCargo1);
            Assert.Null(cesCargo.IdCargo2);
            Assert.Null(cesCargo.CesCargo1);
            Assert.Null(cesCargo.IdSecretaria);
            Assert.Null(cesCargo.IdJobId);
            Assert.Null(cesCargo.EsindCargoPublico);
            Assert.Null(cesCargo.EsacumCargo);
            Assert.Null(cesCargo.EscontagemEsp);
            Assert.Null(cesCargo.EsdedicExcl);
            Assert.Null(cesCargo.EsnrLei);
            Assert.Null(cesCargo.EsdtLei);
            Assert.Null(cesCargo.EssitCargo);
            Assert.Null(cesCargo.ElegivelFuncConf);
            Assert.Null(cesCargo.ElegivelAvalCompet);
            Assert.Null(cesCargo.ElegivelAvalMetas);
            Assert.Null(cesCargo.CodCarreira);
        }

        [Fact]
        public void AdicionarCodEmpresaECodCargo_Adiciona_CodEmpresa_e_CodCargo()
        {
            var cesCargo = new CesCargo();

            Assert.Null(cesCargo.CodEmpresa);
            Assert.Null(cesCargo.CodCargo);

            var codEmp = "123";
            var codCarg = "456";
            cesCargo.AdicionarCodEmpresaECodCargo(codEmp, codCarg);

            Assert.Equal(codEmp, cesCargo.CodEmpresa);
            Assert.Equal(codCarg, cesCargo.CodCargo);
        }
    }
}
