﻿using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Services
{
    public interface ICryptoService
    {
        Task<IEnumerable<CryptoValue>> GetCryptoValuesAsync(string ids);

        Task<Response> AddCryptoToDbAsync(CryptocurrencyDTO crypto);

        Task<CryptoPortfolio> GetCryptoPortfolioAsync();
    }
}