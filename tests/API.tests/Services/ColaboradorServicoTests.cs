using System.Net;
using API.Core.Exceptions;
using API.modelos.InputModels.Colaborador;
using API.modelos.InputModels.DadosBancarios;
using API.modelos.InputModels.Miscellaneous;
using API.Servicos;
using API.Servicos.Globais;
using AutoMapper;
using Dominio.Core.Repositorios;
using Dominio.Entidades;
using GenFu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace tests.API.tests.Services
{
    public class ColaboradorServicoTests
    {
        private Mock<IConfiguration> _config;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapper;
        private Mock<IHttpContextAccessor> _httpContext;
        private Mock<IMatriculaServico> _matriculaServico;
        private Mock<IFilialServico> _filialServico;
        private Mock<ITipoPessoaServico> _tipoPessoaServico;
        private Mock<ICesCargoServico> _cesCargoServico;
        private Mock<IRglGrupoHorarioServico> _rglGrupoHorarioServico;
        private Mock<IRglHorarioServico> _rglHorarioServico;
        private Mock<IPessoaFisFuncServico> _pessoaFisFuncServico;
        private Mock<IPessoaFisicaServico> _pessoaFisicaServico;
        private Mock<IPessoaFuncServico> _pessoaFuncServico;
        private Mock<IPessoaPessServico> _pessoaPessServico;
        private Mock<ILotacaoServico> _lotacaoServico;
        private Mock<IControleGlobalServico> _controleGlobalServico;
        private Mock<ISindicatoServico> _sindicatoServico;
        private Mock<IEmpresaDescritorServico> _empresaDescritorServico;
        private Mock<IBancoAgenciaServico> _bancoAgenciaServico;
        private Mock<IHistCodGfipServico> _histCodGfipServico;
        private Mock<IHistLotacaoServico> _histLotacaoServico;
        private Mock<ISituacaoPessoaServico> _situacaoPessoaServico;
        private Mock<IPessoaJuridicaServico> _pessoaJuridicaServico;
        private Mock<ISituacaoServico> _situacaoServico;
        private Mock<IEmpresaServico> _empresaServico;
        private Mock<IParametroInstalacaoServico> _parametroInstalacaoServico;
        private Mock<IParametroGlobalServico> _parametroGlobalServico;
        private Mock<IBancoServico> _bancoServico;

        private ColaboradorServico colaboradorServico;

        public ColaboradorServicoTests()
        {
            _httpContext = new Mock<IHttpContextAccessor>();
            _config = new Mock<IConfiguration>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _mapper = new Mock<IMapper>();
            _matriculaServico = new Mock<IMatriculaServico>();
            _filialServico = new Mock<IFilialServico>();
            _tipoPessoaServico = new Mock<ITipoPessoaServico>();
            _cesCargoServico = new Mock<ICesCargoServico>();
            _rglGrupoHorarioServico = new Mock<IRglGrupoHorarioServico>();
            _rglHorarioServico = new Mock<IRglHorarioServico>();
            _pessoaFisFuncServico = new Mock<IPessoaFisFuncServico>();
            _pessoaFisicaServico = new Mock<IPessoaFisicaServico>();
            _pessoaFuncServico = new Mock<IPessoaFuncServico>();
            _pessoaPessServico = new Mock<IPessoaPessServico>();
            _lotacaoServico = new Mock<ILotacaoServico>();
            _controleGlobalServico = new Mock<IControleGlobalServico>();
            _sindicatoServico = new Mock<ISindicatoServico>();
            _empresaDescritorServico = new Mock<IEmpresaDescritorServico>();
            _bancoAgenciaServico = new Mock<IBancoAgenciaServico>();
            _histCodGfipServico = new Mock<IHistCodGfipServico>();
            _histLotacaoServico = new Mock<IHistLotacaoServico>();
            _situacaoPessoaServico = new Mock<ISituacaoPessoaServico>();
            _pessoaJuridicaServico = new Mock<IPessoaJuridicaServico>();
            _situacaoServico = new Mock<ISituacaoServico>();
            _empresaServico = new Mock<IEmpresaServico>();
            _parametroInstalacaoServico = new Mock<IParametroInstalacaoServico>();
            _parametroGlobalServico = new Mock<IParametroGlobalServico>();
            _bancoServico = new Mock<IBancoServico>();

            colaboradorServico = new ColaboradorServico(
                _config.Object,
                _unitOfWork.Object,
                _mapper.Object,
                _httpContext.Object,
                _matriculaServico.Object,
                _filialServico.Object,
                _tipoPessoaServico.Object,
                _cesCargoServico.Object,
                _rglGrupoHorarioServico.Object,
                _rglHorarioServico.Object,
                _pessoaFisFuncServico.Object,
                _pessoaFisicaServico.Object,
                _pessoaFuncServico.Object,
                _pessoaPessServico.Object,
                _lotacaoServico.Object,
                _controleGlobalServico.Object,
                _sindicatoServico.Object,
                _empresaDescritorServico.Object,
                _bancoAgenciaServico.Object,
                _histCodGfipServico.Object,
                _histLotacaoServico.Object,
                _situacaoPessoaServico.Object,
                _pessoaJuridicaServico.Object,
                _empresaServico.Object,
                _situacaoServico.Object,
                _parametroInstalacaoServico.Object,
                _parametroGlobalServico.Object,
                _bancoServico.Object
            );
        }

        [Fact]
        public async void CriaPrimeiroColaboradorPessoaFisica_ImportarColaboradorAtualizando()
        {
            int lCodPessoaEsperado = 1;

            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();

            GerarContextRoles("ADMIN");

            _controleGlobalServico
                .Setup(x => x.UpsertControleGlobal())
                .Returns(Task.FromResult(lCodPessoaEsperado));
            var matriculaExternaFake = A.New<MatriculaExterna>();
            await TestCriarColaboradorPeloTipoDePessoa_ImportarColaboradorAtualizando(
                1,
                lCodPessoaEsperado,
                matriculaExternaFake,
                colaborador, 2
            );
            _controleGlobalServico.Verify(x => x.UpsertControleGlobal(), Times.Once);
            _pessoaFisicaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(1, colaborador),
                Times.Once
            );
            _pessoaJuridicaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(It.IsAny<int>(), colaborador),
                Times.Never
            );
        }



        [Fact]
        public async void CriaPrimeiroColaboradorPessoaJuridica_ImportarColaboradorAtualizando()
        {
            int lCodPessoaEsperado = 1;
            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();
            _controleGlobalServico
                .Setup(x => x.UpsertControleGlobal())
                .Returns(Task.FromResult(lCodPessoaEsperado));
            var matriculaExternaFake = A.New<MatriculaExterna>();

            GerarContextRoles("ADMIN");

            await TestCriarColaboradorPeloTipoDePessoa_ImportarColaboradorAtualizando(
                2,
                lCodPessoaEsperado,
                matriculaExternaFake,
                colaborador, 2
            );
            _controleGlobalServico.Verify(x => x.UpsertControleGlobal(), Times.Once);
            _pessoaFisicaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(1, colaborador),
                Times.Never
            );
            _pessoaJuridicaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(It.IsAny<int>(), colaborador),
                Times.Once
            );
        }

        [Fact]
        public async void AtualizaColaboradorQueJaExistePessoaFisica_ImportarColaboradorAtualizando()
        {
            int lCodPessoaEsperado = 2;

            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();

            A.Set<MatriculaExterna>().Fill(x => x.MteCodPessoa, lCodPessoaEsperado);
            var matriculaExternaFake = A.New<MatriculaExterna>();
            _matriculaServico
                .Setup(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            colaborador.CodEmpresa,
                            It.IsAny<int>(),
                            It.IsAny<string>(),
                            (int)colaborador.NroMatricula
                        )
                )
                .ReturnsAsync(matriculaExternaFake);

            GerarContextRoles("ADMIN");

            await TestCriarColaboradorPeloTipoDePessoa_ImportarColaboradorAtualizando(
                1,
                lCodPessoaEsperado,
                matriculaExternaFake,
                colaborador, 2
            );
            _controleGlobalServico.Verify(x => x.UpsertControleGlobal(), Times.Never);
            _pessoaFisicaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoaEsperado, colaborador),
                Times.Once
            );
            _pessoaJuridicaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(It.IsAny<int>(), colaborador),
                Times.Never
            );
            _unitOfWork.Verify(x => x.Comitar(), Times.Once);
        }

        [Fact]
        public async void LevantaErroAoImportarColaboradorComEmpresaInvaida_ImportarColaboradorAtualizando()
        {
            /// Setup
            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();

            string mensagemErro = "NONE SHALL PASS";
            _empresaServico
                .Setup(x => x.ValidaEmpresa(colaborador.CodEmpresa))
                .Throws(new Exception(mensagemErro));
            /// Act
            try
            {
                await colaboradorServico.ImportarColaboradorAtualizando(colaborador);
            }
            catch (Exception ex)
            {
                /// Assert
                Assert.Equal(ex.Message, mensagemErro);
                _unitOfWork.Verify(x => x.Comitar(), Times.Never);
            }
        }

        [Fact]
        public async void LevantaErroAoImportarColaboradorComSituacaoInvaida_ImportarColaboradorAtualizando()
        {
            /// Setup
            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();

            string mensagemErro = "NONE SHALL PASS";
            _situacaoServico
                .Setup(x => x.ValidaSituacao(colaborador.Situacao))
                .Throws(new Exception(mensagemErro));
            /// Act
            try
            {
                await colaboradorServico.ImportarColaboradorAtualizando(colaborador);
            }
            catch (Exception ex)
            {
                /// Assert
                Assert.Equal(ex.Message, mensagemErro);
                _unitOfWork.Verify(x => x.Comitar(), Times.Never);
            }
        }

        [Fact]
        public async void LevantaErroQuandoMatriculaExternaNaoPossuiCodPessoa_ImportarColaboradorAtualizando()
        {
            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();

            PrazosContratos prazos = new PrazosContratos(
                $"{colaborador.Situacao.ToString()}-060030;2-045015;3-145045;"
            );
            var numeracaoMatriculasFake = "0";
            var pEmpCodCargoFake = "Foo";
            var pEmpCodHorarFake = "HoraDeAventura";
            var pEmpCodLotacFake = "CasaDosAnoes";

            SetupConfiguracoes(
                prazos,
                numeracaoMatriculasFake,
                pEmpCodCargoFake,
                pEmpCodHorarFake,
                pEmpCodLotacFake
            );


            string codAgrMatrMock = "123";
            TipoPessoa tipoPessoa = A.New<TipoPessoa>();
            tipoPessoa.Atualizar(tppCodAgrupMatr: codAgrMatrMock, tppTipoFisJur: 1);
            _tipoPessoaServico
                .Setup(x => x.BuscarTipoPessoaPorCodTipoPessoa(It.IsAny<string>()))
                .Returns(Task.FromResult<TipoPessoa?>(tipoPessoa));

            A.Set<MatriculaExterna>().Fill(x => x.MteCodPessoa, (int?)null);
            MatriculaExterna matExtFake = A.New<MatriculaExterna>();

            _matriculaServico
                .Setup(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            colaborador.CodEmpresa,
                            It.IsAny<int>(),
                            codAgrMatrMock,
                            (int)colaborador.NroMatricula
                        )
                )
                .ReturnsAsync(matExtFake);

            try
            {
                await colaboradorServico.ImportarColaboradorAtualizando(colaborador);
                Assert.Fail("Esperava Lançar HttpErroDeUsuario");
            }
            catch (HttpErroDeUsuario ex)
            {
                Assert.Equal(
                    ex.Message,
                    $"Matricula Externa com código de agrupamento: {matExtFake.MteCodAgrupMatr} não possui codigo de pessoa."
                );
                _unitOfWork.Verify(x => x.Comitar(), Times.Never);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Levantou um erro inesperado com mensagem: {ex.Message}");
            }
        }

        [Fact]
        public async void LevantaErroQuandoNaoEncontraContratoDePrazos_ImportarColaboradorAtualizando()
        {
            ImportarColaboradorInputModel colaborador = CriarColaboradorFake();
            colaborador.TipoContrato = Faker.RandomNumber.Next(4, 100).ToString();
            PrazosContratos prazos = new PrazosContratos($"0-060030;2-045015;3-145045;");
            var numeracaoMatriculasFake = "0";
            var pEmpCodCargoFake = "Foo";
            var pEmpCodHorarFake = "HoraDeAventura";
            var pEmpCodLotacFake = "CasaDosAnoes";

            SetupConfiguracoes(
                prazos,
                numeracaoMatriculasFake,
                pEmpCodCargoFake,
                pEmpCodHorarFake,
                pEmpCodLotacFake
            );

            string codAgrMatrMock = "123";
            TipoPessoa tipoPessoa = A.New<TipoPessoa>();
            tipoPessoa.Atualizar(tppCodAgrupMatr: codAgrMatrMock, tppTipoFisJur: 1);
            _tipoPessoaServico
                .Setup(x => x.BuscarTipoPessoaPorCodTipoPessoa(It.IsAny<string>()))
                .Returns(Task.FromResult<TipoPessoa?>(tipoPessoa));

            _controleGlobalServico.Setup(x => x.UpsertControleGlobal()).Returns(Task.FromResult(1));

            var matriculaExternaFake = A.New<MatriculaExterna>();
            _matriculaServico
                .Setup(
                    x =>
                        x.CriarOuAtualizarParaImportacao(
                            It.IsAny<string>(),
                            It.IsAny<int>(),
                            It.IsAny<string>(),
                            colaborador, 0
                        )
                )
                .Returns(Task.FromResult(matriculaExternaFake));

            try
            {
                await colaboradorServico.ImportarColaboradorAtualizando(colaborador);
                Assert.Fail("Deveria levantar erro de usuário com conflito");
            }
            catch (HttpErroDeUsuario ex)
            {
                Assert.Equal(
                    ex.Message,
                    $"Não existem prazos de contratos registrados para o tipo de contrato: {colaborador.TipoContrato}"
                );
                Assert.Equal(ex.codigo, HttpStatusCode.Conflict);
                _unitOfWork.Verify(x => x.Comitar(), Times.Once);
                AssertsParametrosAuxiliares(
                    colaborador,
                    pEmpCodCargoFake,
                    pEmpCodHorarFake,
                    pEmpCodLotacFake
                );
            }
            catch (Exception ex)
            {
                Assert.Fail($"Levantou um erro inesperado com mensagem: {ex.Message}");
            }
        }

        [Fact]
        public async void EncontraCodAgrp_BuscarCodAgrpMatr()
        {
            var tipoDePessoa = "Batman";
            short numeroFilial = 1;
            var codEmpresa = "001";
            TipoPessoa tipoPessoaFake = A.New<TipoPessoa>();
            _tipoPessoaServico
                .Setup(x => x.BuscarTipoPessoaPorCodTipoPessoa(tipoDePessoa))
                .ReturnsAsync(tipoPessoaFake);

            string CodAgrMatrRetornado = await colaboradorServico.BuscarCodAgrpMatr(
                tipoDePessoa,
                numeroFilial,
                codEmpresa
            );

            Assert.Equal(tipoPessoaFake.TppCodAgrupMatr, CodAgrMatrRetornado);
            _tipoPessoaServico.Verify(
                x => x.BuscarTipoPessoaPorCodTipoPessoa(tipoDePessoa),
                Times.Once
            );
        }

        [Fact]
        public async void RetornaPadraoParaColaboradorSemTipoDePessoa_BuscarCodAgrMatr()
        {
            var tipoDePessoa = "Batman";
            short numeroFilial = 1;
            var codEmpresa = "001";

            var RetornoEsperado = "01";

            string CodAgrMatrRetornado = await colaboradorServico.BuscarCodAgrpMatr(
                tipoDePessoa,
                numeroFilial,
                codEmpresa
            );

            Assert.Equal(RetornoEsperado, CodAgrMatrRetornado);
            _tipoPessoaServico.Verify(
                x => x.BuscarTipoPessoaPorCodTipoPessoa(tipoDePessoa),
                Times.Once
            );
        }

        [Fact]
        public async void EncontraCodPessoaPelaMatriculaExternaComNroFilial_BuscarCodPessoa()
        {
            var tipoDePessoa = Faker.RandomNumber.Next(1, 100).ToString();
            short nroFilial = (short)Faker.RandomNumber.Next(1, 100);
            string codEmpresa = Faker.RandomNumber.Next(1, 100).ToString();
            long nroMatricula = Faker.RandomNumber.Next(1, 100);
            string codAgrMatr = Faker.RandomNumber.Next(1, 1000).ToString();
            string numeracaoMatriculas = Faker.RandomNumber.Next(2, short.MaxValue).ToString();

            A.Reset<MatriculaExterna>();
            MatriculaExterna matriculaFake = A.New<MatriculaExterna>();
            _parametroInstalacaoServico
                .SetupGet(x => x.numeracaoMatriculas)
                .Returns(numeracaoMatriculas);
            _matriculaServico
                .Setup(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            codEmpresa,
                            nroFilial,
                            codAgrMatr,
                            (int)nroMatricula
                        )
                )
                .ReturnsAsync(matriculaFake);

            var retorno = await colaboradorServico.BuscarCodPessoa(
                tipoDePessoa,
                nroFilial,
                codEmpresa,
                nroMatricula,
                codAgrMatr
            );

            _parametroInstalacaoServico.Verify(x => x.numeracaoMatriculas, Times.AtMost(2));
            _matriculaServico.Verify(
                x =>
                    x.BuscarMatriculaExternaPorMultiplasChaves(
                        codEmpresa,
                        nroFilial,
                        codAgrMatr,
                        (int)nroMatricula
                    ),
                Times.Once
            );
            Assert.Equal(retorno, matriculaFake.MteCodPessoa);
        }

        /*
        /  Você pode até achar que esse teste não faz sentido, mas ele faz por conta da lógica de negócio. ;D
        /  Não foi criado um método genérico para testar EncontraCodPessoaPelaMatriculaExternaComNroFilial_BuscarCodPessoa e EncontraCodPessoaPelaMatriculaExternaZeroFilial_BuscarCodPessoa
        /  com o objetivo de deixar clara a inteção do teste.
        */
        [Fact]
        public async void EncontraCodPessoaPelaMatriculaExternaZeroFilial_BuscarCodPessoa()
        {
            var tipoDePessoa = Faker.RandomNumber.Next(1, 100).ToString();
            short nroFilial = (short)Faker.RandomNumber.Next(1, 100);
            string codEmpresa = Faker.RandomNumber.Next(1, 100).ToString();
            long nroMatricula = Faker.RandomNumber.Next(1, 100);
            string codAgrMatr = Faker.RandomNumber.Next(1, 1000).ToString();
            string numeracaoMatriculas = Faker.RandomNumber.Next(2, short.MaxValue).ToString();
            A.Reset<MatriculaExterna>();
            MatriculaExterna matriculaFake = A.New<MatriculaExterna>();

            _parametroInstalacaoServico
                .SetupGet(x => x.numeracaoMatriculas)
                .Returns(numeracaoMatriculas);
            _matriculaServico
                .Setup(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            codEmpresa,
                            nroFilial,
                            codAgrMatr,
                            (int)nroMatricula
                        )
                )
                .ReturnsAsync(matriculaFake);

            var retorno = await colaboradorServico.BuscarCodPessoa(
                tipoDePessoa,
                nroFilial,
                codEmpresa,
                nroMatricula,
                codAgrMatr
            );

            _parametroInstalacaoServico.Verify(x => x.numeracaoMatriculas, Times.AtMost(2));
            _matriculaServico.Verify(
                x =>
                    x.BuscarMatriculaExternaPorMultiplasChaves(
                        codEmpresa,
                        nroFilial,
                        codAgrMatr,
                        (int)nroMatricula
                    ),
                Times.Once
            );
            Assert.Equal(retorno, matriculaFake.MteCodPessoa);
        }

        [Fact]
        public async void LevantaErroParaMatriculaExternaSemCodPessoa_BuscarCodPessoa()
        {
            var tipoDePessoa = Faker.RandomNumber.Next(1, 100).ToString();
            short nroFilial = (short)Faker.RandomNumber.Next(1, 100);
            string codEmpresa = Faker.RandomNumber.Next(1, 100).ToString();
            long nroMatricula = Faker.RandomNumber.Next(1, 100);
            string codAgrMatr = Faker.RandomNumber.Next(1, 1000).ToString();
            string numeracaoMatriculas = "0001";

            A.Set<MatriculaExterna>().Fill(mat => mat.MteCodPessoa, (int?)null);
            MatriculaExterna matriculaFake = A.New<MatriculaExterna>();

            short nroFilialEsperadoASerUsado = 0;

            _parametroInstalacaoServico
                .SetupGet(x => x.numeracaoMatriculas)
                .Returns(numeracaoMatriculas);
            _matriculaServico
                .Setup(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            codEmpresa,
                            nroFilialEsperadoASerUsado,
                            codAgrMatr,
                            (int)nroMatricula
                        )
                )
                .ReturnsAsync(matriculaFake);

            try
            {
                await colaboradorServico.BuscarCodPessoa(
                    tipoDePessoa,
                    nroFilialEsperadoASerUsado,
                    codEmpresa,
                    nroMatricula,
                    codAgrMatr
                );
                Assert.Fail("Esperava levantar exceção HttpErroDeUsuario");
            }
            catch (HttpErroDeUsuario ex)
            {
                _parametroInstalacaoServico.Verify(x => x.numeracaoMatriculas, Times.AtMost(2));
                _matriculaServico.Verify(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            codEmpresa,
                            nroFilialEsperadoASerUsado,
                            codAgrMatr,
                            (int)nroMatricula
                        ),
                    Times.Once
                );
                Assert.Equal(
                    ex.Message,
                    $"Matricula Externa com código de agrupamento: {matriculaFake.MteCodAgrupMatr} não possui codigo de pessoa."
                );
                Assert.Equal(ex.codigo, HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Levantou um erro inesperado com mensagem: {ex.Message}");
            }
        }

        [Fact]
        public async void LevantaErroSeParametroInstalacaoNaoFoiDefinido_BuscarCodPessoa()
        {
            var tipoDePessoa = Faker.RandomNumber.Next(1, 100).ToString();
            short nroFilial = (short)Faker.RandomNumber.Next(1, 100);
            string codEmpresa = Faker.RandomNumber.Next(1, 100).ToString();
            long nroMatricula = Faker.RandomNumber.Next(1, 100);
            string codAgrMatr = Faker.RandomNumber.Next(1, 1000).ToString();

            A.Set<MatriculaExterna>().Fill(mat => mat.MteCodPessoa, (int?)null);
            MatriculaExterna matriculaFake = A.New<MatriculaExterna>();

            short nroFilialEsperadoASerUsado = 0;
            try
            {
                await colaboradorServico.BuscarCodPessoa(
                    tipoDePessoa,
                    nroFilialEsperadoASerUsado,
                    codEmpresa,
                    nroMatricula,
                    codAgrMatr
                );
                Assert.Fail("Esperava levantar exceção HttpErroDeServidor");
            }
            catch (HttpErroDeServidor ex)
            {
                _parametroInstalacaoServico.Verify(x => x.numeracaoMatriculas, Times.AtMost(2));
                _matriculaServico.Verify(
                    x =>
                        x.BuscarMatriculaExternaPorMultiplasChaves(
                            It.IsAny<string>(),
                            It.IsAny<int>(),
                            It.IsAny<string>(),
                            It.IsAny<int>()
                        ),
                    Times.Never
                );
                Assert.Equal(
                    ex.Message,
                    "Parametros de instalação não foram definidos corretamente"
                );
                Assert.Equal(ex.codigo, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                Assert.Fail(
                    $"Esperava levantar HttpErroDeServidor mas levantou erro com a mensagem: {ex.Message}"
                );
            }
        }

        private void GerarContextRoles(string v)
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Roles"] = "ADMIN";
            _httpContext.Setup(_ => _.HttpContext).Returns(httpContext);
        }

        /// <summary>
        /// Esse teste é genérico para não ser necessário repetir todas as configurações e mudar só o número do tipo de pessoa.
        ///  1 = Pessoa Física, 2 = pessoa jurídica
        /// </summary>
        private async Task TestCriarColaboradorPeloTipoDePessoa_ImportarColaboradorAtualizando(
            short Pessoa,
            int lCodPessoaEsperado,
            MatriculaExterna matriculaExternaFake,
            ImportarColaboradorInputModel colaborador,
            short mteCandidato = 0
        )
        {
            // Setup
            PrazosContratos prazos = new PrazosContratos(
                $"{colaborador.TipoContrato}-060030;2-045015;3-145045;"
            );
            var numeracaoMatriculasFake = "0";
            var pEmpCodCargoFake = "Foo";
            var pEmpCodHorarFake = "HoraDeAventura";
            var pEmpCodLotacFake = "CasaDosAnoes";

            SetupConfiguracoes(
                prazos,
                numeracaoMatriculasFake,
                pEmpCodCargoFake,
                pEmpCodHorarFake,
                pEmpCodLotacFake

            );

            string codAgrMatrMock = "123";
            TipoPessoa tipoPessoa = A.New<TipoPessoa>();
            tipoPessoa.Atualizar(tppCodAgrupMatr: codAgrMatrMock, tppTipoFisJur: Pessoa);
            _tipoPessoaServico
                .Setup(x => x.BuscarTipoPessoaPorCodTipoPessoa(It.IsAny<string>()))
                .Returns(Task.FromResult<TipoPessoa?>(tipoPessoa));

            _matriculaServico
                .Setup(
                    x =>
                            x.CriarOuAtualizarParaImportacao(
                                It.IsAny<string>(),
                                It.IsAny<int>(),
                                It.IsAny<string>(),
                                colaborador, mteCandidato
                            )
                    )
                    .Returns(Task.FromResult<MatriculaExterna>(matriculaExternaFake));

            // Act

            var resultado = await colaboradorServico.ImportarColaboradorAtualizando(colaborador);
            // Assert
            Assert.Equal(matriculaExternaFake, resultado);

            _unitOfWork.Verify(x => x.Comitar(), Times.AtMost(2));

            AssertsParametrosAuxiliares(
                colaborador,
                pEmpCodCargoFake,
                pEmpCodHorarFake,
                pEmpCodLotacFake
            );
            AssertsCriacoesGeraisColaborador(
                colaborador,
                lCodPessoaEsperado,
                codAgrMatrMock,
                numeracaoMatriculasFake,
                prazos,
                mteCandidato
            );
        }

        private ImportarColaboradorInputModel CriarColaboradorFake()
        {
            ImportarColaboradorInputModel colaborador = A.New<ImportarColaboradorInputModel>();
            colaborador.DadosPagamentoColaborador = A.New<DadosPagamentoColaboradorInputModel>();
            colaborador.DadosFgts = A.New<DadosFgtsInputModel>();
            colaborador.Situacao = 1;
            colaborador.TipoContrato = Faker.RandomNumber.Next(1, 10).ToString();
            return colaborador;
        }

        private void SetupConfiguracoes(
            PrazosContratos prazos,
            string numeracaoMatriculasFake,
            string pEmpCodCargoFake,
            string pEmpCodHorarFake,
            string pEmpCodLotacFake
        )
        {

            _parametroGlobalServico
                .Setup(x => x.DefinirPrazosContratos())
                        .Returns(Task.FromResult(prazos));
            _parametroGlobalServico.SetupGet(x => x.prazosContratos).Returns(prazos);
            _parametroInstalacaoServico
                .SetupGet(x => x.numeracaoMatriculas)
                        .Returns(numeracaoMatriculasFake);
            _empresaDescritorServico.SetupGet(x => x.pEmpCodCargo).Returns(pEmpCodCargoFake);
            _empresaDescritorServico.SetupGet(x => x.pEmpCodHorar).Returns(pEmpCodHorarFake);
            _empresaDescritorServico.SetupGet(x => x.pEmpCodLotac).Returns(pEmpCodLotacFake);
        }

        private void AssertsParametrosAuxiliares(
            ImportarColaboradorInputModel colaborador,
            string pEmpCodCargoFake,
            string pEmpCodHoraFake,
            string pEmpCodLotacFake
        )
        {
            _empresaDescritorServico.Verify(
                x => x.RegistrarParametrosEmpresa(colaborador.CodEmpresa),
                Times.Once
            );
            _parametroInstalacaoServico.Verify(
                x => x.DefinirNumeracaoMatriculas(colaborador.NroFilial),
                Times.Once
            );
            _parametroGlobalServico.Verify(x => x.DefinirPrazosContratos(), Times.Once);
            _sindicatoServico.Verify(x => x.Validar(colaborador.CodSind), Times.Once);
            _cesCargoServico.Verify(
                x => x.Validar(pEmpCodCargoFake, colaborador.CodCargo),
                Times.Once
            );
            _rglGrupoHorarioServico.Verify(x => x.Validar(pEmpCodHoraFake), Times.Once);
            _rglHorarioServico.Verify(
                x => x.Validar(pEmpCodHoraFake, colaborador.CodHorario),
                Times.Once
            );
            _bancoAgenciaServico.Verify(
                x => x.Validar(colaborador.DadosFgts.BancoFgts),
                Times.Once
            );
            _bancoAgenciaServico.Verify(
                x => x.Validar(colaborador.DadosPagamentoColaborador.CodigoBancoPag),
                Times.Once
            );
            _lotacaoServico.Verify(
                x => x.Validar(pEmpCodLotacFake, colaborador.CodLotacao),
                Times.Once
            );
            _filialServico.Verify(
                x => x.Validar(colaborador.CodEmpresa, colaborador.NroFilial),
                Times.Once
            );
        }

        private void AssertsCriacoesGeraisColaborador(
            ImportarColaboradorInputModel colaborador,
            int lCodPessoa,
            string codAgrMatrMock,
            string numeracaoMatriculasFake,
            PrazosContratos prazos,
            short mteCandidato = 0
        )
        {
            _matriculaServico.Verify(
                x =>
                    x.CriarOuAtualizarParaImportacao(
                        codAgrMatrMock,
                        lCodPessoa,
                        numeracaoMatriculasFake,
                        colaborador, mteCandidato
                    ),
                Times.Once
            );
            _histCodGfipServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoa, colaborador),
                Times.Once
            );
            _histLotacaoServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoa, colaborador),
                Times.Once
            );
            _situacaoPessoaServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoa, colaborador),
                Times.Once
            );
            _pessoaFisFuncServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoa, colaborador),
                Times.Once
            );
            _tipoPessoaServico.Verify(
                x => x.BuscarTipoPessoaPorCodTipoPessoa(colaborador.TipoPessoa)
            );
            _pessoaFuncServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoa, prazos.Prazos[0], colaborador),
                Times.Once
            );
            _pessoaPessServico.Verify(
                x => x.CriarOuAtualizarParaImportacao(lCodPessoa, colaborador),
                Times.Once
            );
        }
    }
}
