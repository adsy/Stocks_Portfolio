using MediatR;
using Services.Crypto.Querys;
using Services.Interfaces.Services;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.CryptoHandler
{
    public class GetCryptoPortfolioAsyncHandler : IRequestHandler<GetCryptoPortfolioAsyncQuery, CryptoPortfolio>
    {
        private readonly ICryptoService _cryptoService;

        public GetCryptoPortfolioAsyncHandler(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(ICryptoService));
        }

        public async Task<CryptoPortfolio> Handle(GetCryptoPortfolioAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = await _cryptoService.GetCryptoPortfolioAsync();

            return result;
        }
    }
}