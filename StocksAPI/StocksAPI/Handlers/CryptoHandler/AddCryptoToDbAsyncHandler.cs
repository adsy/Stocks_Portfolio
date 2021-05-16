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
    public class AddCryptoToDbAsyncHandler : IRequestHandler<AddCryptoToDbAsyncCommand, Response>
    {
        private readonly ICryptoService _cryptoService;

        public AddCryptoToDbAsyncHandler(ICryptoService cryptoService)
        {
            _cryptoService = cryptoService ?? throw new ArgumentNullException(nameof(ICryptoService));
        }

        public Task<Response> Handle(AddCryptoToDbAsyncCommand request, CancellationToken cancellationToken)
        {
            var result = _cryptoService.AddCryptoToDbAsync(request.crypto);

            return result;
        }
    }
}