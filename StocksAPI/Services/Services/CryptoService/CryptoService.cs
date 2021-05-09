using Services.Interfaces.Repository;
using Services.Interfaces.Services;
using Services.Models;
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
    }
}