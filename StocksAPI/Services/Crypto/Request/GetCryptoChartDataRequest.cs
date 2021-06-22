using MediatR;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Crypto.Request
{
    public class GetCryptoChartDataRequest : IRequest<Response<CryptoChartData>>
    {
        public string Id { get; set; }
    }
}