using MediatR;
using Services;
using Services.Crypto.Commands;
using Services.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockAPI.Handlers.CryptoHandler
{
    public class RemoveCryptoFromDbAsyncHandler : IRequestHandler<RemoveCryptoFromDbAsyncCommand, Response>
    {
        private readonly ICryptoService _cryptoService;

        public RemoveCryptoFromDbAsyncHandler(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(cryptoService));
        }

        public async Task<Response> Handle(RemoveCryptoFromDbAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = await _cryptoService.RemoveCryptoFromDbAsync(request.cryptocurrencyDTO);

            return result;
        }
    }
}