using MediatR;
using Services;
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
    public class GetCryptoValuesAsyncHandler : IRequestHandler<GetCryptoValuesAsyncQuery, Response<IEnumerable<CryptoValue>>>
    {
        private readonly ICryptoService _cryptoService;

        public GetCryptoValuesAsyncHandler(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
        }

        public Task<Response<IEnumerable<CryptoValue>>> Handle(GetCryptoValuesAsyncQuery request, CancellationToken cancellationToken)
        {
            var result = _cryptoService.GetCryptoValuesAsync(request.Ids);

            return result;
        }
    }
}