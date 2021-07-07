using Services.Models;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface ICryptoService
    {
        Task<Response<IEnumerable<CryptoValue>>> GetCryptoValuesAsync(string ids);

        Task<Response> AddCryptoToDbAsync(CryptocurrencyDTO crypto);

        Task<Response<CryptoPortfolio>> GetCryptoPortfolioAsync();

        Task<Response> RemoveCryptoFromDbAsync(CryptocurrencyDTO crypto);

        Task<Response<CryptoChartData>> GetChartDataAsync(string id);

        Task<Response<CryptoSummaryData>> GetCryptoSummaryDataAsync(string id);
    }
}