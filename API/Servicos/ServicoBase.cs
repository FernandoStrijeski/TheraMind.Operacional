using Dominio.Core.Repositorios;
using Infra.Servicos.MultiTenant;
using Microsoft.Extensions.Caching.Memory;

namespace API.Servicos
{
    public class ServicoBase
    {
        private IConfiguration _configuration;
        private IUnitOfWork _unitOfWork;
        private IMemoryCache _memoryCache;
        private MemoryCacheEntryOptions _memoryCacheEntryOptions;
        private IConnectionParamsServico _connectionParamsServico;
        protected string IdentificadorDeSessao;

        public ServicoBase(IUnitOfWork unitOfWork)
        {            
            _unitOfWork = unitOfWork;
        }

        public ServicoBase(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public ServicoBase(
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IMemoryCache memoryCache,
            IConnectionParamsServico connectionParamsServico
        )
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _connectionParamsServico = connectionParamsServico;
            IdentificadorDeSessao = _connectionParamsServico.ObterConnectionsParams().Alias;

            _memoryCacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(2),
                Size = 1024,
            };
        }

        public async Task Comitar()
        {
            await _unitOfWork.Comitar();
        }

        public Task Cachear<T>(string chave, T valor)
        {
            _memoryCache.Set(chave, valor, _memoryCacheEntryOptions);
            return Task.CompletedTask;
        }

        public Task<T?> BuscarCache<T>(string chave)
        {
            return _memoryCache.TryGetValue(chave, out T valor)
                ? Task.FromResult(valor)
                : Task.FromResult<T>(default(T));
        }
    }
}
