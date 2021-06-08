using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockSummaryData
    {
        public string FullName { get; set; }
        public double OpenPrice { get; set; }
        public double DayHigh { get; set; }
        public double DayLow { get; set; }
        public double YearHigh { get; set; }
        public double YearLow { get; set; }
        public string MarketCap { get; set; }
        public double PercentChange { get; set; }
    }
}