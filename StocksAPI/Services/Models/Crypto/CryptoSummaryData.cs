using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoSummaryData
    {
        public double High24H { get; set; }
        public double Low24H { get; set; }
        public double PriceChangePercentage24H { get; set; }
        public double MarketCapChange24H { get; set; }
        public double MarketCapChangePercentage24H { get; set; }
        public double MarketCapRank { get; set; }
    }
}