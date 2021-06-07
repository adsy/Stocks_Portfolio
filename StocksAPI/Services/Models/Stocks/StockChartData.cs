using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockChartData
    {
        public StockChartData()
        {
            TimestampData = new Dictionary<int, string>();
            StockPriceData = new List<long>();
        }

        public Dictionary<int, string> TimestampData { get; set; }
        public List<long> StockPriceData { get; set; }
    }
}