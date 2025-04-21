using Dominio.Entidades;
using Xunit;

namespace tests.Dominio.tests.Entidades
{
    public class SindicatoTests
    {
        public SindicatoTests() { }

        [Fact]
        public void CriarParaImportacao_CriaNovoSindicato()
        {
            // Setup
            string sCodSindicato = "123";
            // Act
            var sindicato = Sindicato.CriarParaImportacao(sCodSindicato);
            // Assert

            Assert.Equal(sCodSindicato, sindicato.SinCodSindicato);
            Assert.Equal("Gerado na Importação", sindicato.SinNomeSindicato);
            Assert.Null(sindicato.SinDataDissidio);
            Assert.Null(sindicato.SinMesContribSind);
            Assert.Null(sindicato.SinEndRua);
            Assert.Null(sindicato.SinEndNumero);
            Assert.Null(sindicato.SinEndComplem);
            Assert.Null(sindicato.SinEndBairro);
            Assert.Null(sindicato.SinEndCidade);
            Assert.Null(sindicato.SinEndEstado);
            Assert.Null(sindicato.SinEndCep);
            Assert.Null(sindicato.SinTelefone);
            Assert.Null(sindicato.SinRamal);
            Assert.Null(sindicato.SinFax);
            Assert.Null(sindicato.SinDirigenteSindical);
            Assert.Null(sindicato.SinReserva);
            Assert.Null(sindicato.SinCgccompleto);
            Assert.Null(sindicato.SinEstabilLicMater);
            Assert.Null(sindicato.SinCodigoSindical);
            Assert.Null(sindicato.SinHabilitado);
            Assert.Null(sindicato.SinForCalcMed13);
            Assert.Null(sindicato.SinForApurBase13);
            Assert.Null(sindicato.SinQtdeMeses13);
            Assert.Null(sindicato.SinMaioresMeses13);
            Assert.Null(sindicato.SinForCalcMedFer);
            Assert.Null(sindicato.SinForApurBaseFer);
            Assert.Null(sindicato.SinQtdeMesesFer);
            Assert.Null(sindicato.SinMaioresMesesFer);
            Assert.Null(sindicato.SinForCalcMedAvPrv);
            Assert.Null(sindicato.SinForApurBaseAvPrv);
            Assert.Null(sindicato.SinQtdeMesesAvPrv);
            Assert.Null(sindicato.SinMaioresMesesAvPrv);
            Assert.Null(sindicato.SinEstabilServMilitar);
            Assert.Null(sindicato.SinEstabilAfastPrevidenciario);
            Assert.Null(sindicato.SinEstabilAfastAcidentario);
            Assert.Null(sindicato.SinEstabilOutroAfast1);
            Assert.Null(sindicato.SinEstabilOutroAfast2);
            Assert.Null(sindicato.SinEstabilOutroAfast3);
            Assert.Null(sindicato.SinEstabilDirSindic);
        }
    }
}
