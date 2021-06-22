using Services.Interfaces.Repository;
using Services.Interfaces.Services;
using Services.Models;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.CryptoService
{
    public class CryptoService : ICryptoService
    {
        private readonly ICryptoRepository _cryptoRepository;

        public CryptoService(ICryptoRepository cryptoRepository)
        {
            _cryptoRepository = cryptoRepository ?? throw new ArgumentNullException(nameof(cryptoRepository));
        }

        public async Task<IEnumerable<CryptoValue>> GetCryptoValuesAsync(string ids)
        {
            var result = await _cryptoRepository.GetCryptoValuesAsync(ids);

            return result;
        }

        public async Task<Response> AddCryptoToDbAsync(CryptocurrencyDTO crypto)
        {
            var result = await _cryptoRepository.AddCryptoToDbAsync(crypto);

            return result;
        }

        public async Task<CryptoPortfolio> GetCryptoPortfolioAsync()
        {
            var result = await _cryptoRepository.GetCryptoPortfolioAsync();

            return result;
        }

        public async Task<Response> RemoveCryptoFromDbAsync(CryptocurrencyDTO crypto)
        {
            var result = await _cryptoRepository.RemoveCryptoFromDbAsync(crypto);

            return result;
        }

        public async Task<Response<CryptoChartData>> GetChartDataAsync(string id)
        {
            var result = await _cryptoRepository.GetChartDataAsync(id);

            return result;
        }

        public async Task<Response<CryptoSummaryData>> GetCryptoSummaryDataAsync(string id)
        {
            var result = await _cryptoRepository.GetCryptoSummaryDataAsync(id);

            return result;
        }
    }
}