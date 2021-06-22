using MediatR;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Crypto.Request
{
    public class GetCryptoSummaryDataRequest : IRequest<Response<CryptoSummaryData>>
    {
        public string Id { get; set; }
    }
}