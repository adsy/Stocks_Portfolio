using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoChartData
    {
        public CryptoChartData()
        {
            PriceChartData = new List<ChartData>();
        }

        public List<ChartData> PriceChartData { get; set; }
    }
}