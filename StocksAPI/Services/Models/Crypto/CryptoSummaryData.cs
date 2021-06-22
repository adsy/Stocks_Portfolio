using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoSummaryData
    {
        public string High24H { get; set; }
        public string Low24H { get; set; }
        public string PriceChangePercentage24H { get; set; }
        public string MarketCapChange24H { get; set; }
        public string MarketCapChangePercentage24H { get; set; }
        public string MarketCapRank { get; set; }
    }
}