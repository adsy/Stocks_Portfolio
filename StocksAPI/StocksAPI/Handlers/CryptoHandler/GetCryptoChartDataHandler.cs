using MediatR;
using Services;
using Services.Crypto.Request;
using Services.Interfaces.Services;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.CryptoHandler
{
    public class GetCryptoChartDataHandler : IRequestHandler<GetCryptoChartDataRequest, Response<CryptoChartData>>
    {
        private readonly ICryptoService _cryptoService;

        public GetCryptoChartDataHandler(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException($"CrptoChartDataHandler requires cryptoService to not be null.");
        }

        public async Task<Response<CryptoChartData>> Handle(GetCryptoChartDataRequest request, CancellationToken cancellationToken)
        {
            var result = await _cryptoService.GetChartDataAsync(request.Id);

            return result;
        }
    }
}