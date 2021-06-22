using MediatR;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Crypto.Commands
{
    public class RemoveCryptoFromDbAsyncCommand : IRequest<Response>
    {
        public CryptocurrencyDTO cryptocurrencyDTO { get; set; }
    }
}