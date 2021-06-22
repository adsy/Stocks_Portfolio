using Services.Models;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Repository
{
    public interface ICryptoRepository
    {
        Task<IEnumerable<CryptoValue>> GetCryptoValuesAsync(string ids);

        Task<Response> AddCryptoToDbAsync(CryptocurrencyDTO crypto);

        Task<CryptoPortfolio> GetCryptoPortfolioAsync();

        Task<Response> RemoveCryptoFromDbAsync(CryptocurrencyDTO crypto);

        Task<Response<CryptoChartData>> GetChartDataAsync(string id);
    }
}