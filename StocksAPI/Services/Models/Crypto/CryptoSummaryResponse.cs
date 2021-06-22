using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoSummaryResponse
    {
        public string Market_Cap_Rank { get; set; }
        public CryptoMarketData Market_Data { get; set; }
    }
}