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
    public class GetCryptoSummaryDataAsyncHandler : IRequestHandler<GetCryptoSummaryDataRequest, Response<CryptoSummaryData>>
    {
        private readonly ICryptoService _cryptoService;

        public GetCryptoSummaryDataAsyncHandler(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException($"{nameof(GetCryptoSummaryDataAsyncHandler)} requires cryptoService to not be null.");
        }

        public async Task<Response<CryptoSummaryData>> Handle(GetCryptoSummaryDataRequest request, CancellationToken cancellationToken)
        {
            var result = await _cryptoService.GetCryptoSummaryDataAsync(request.Id);

            return result;
        }
    }
}