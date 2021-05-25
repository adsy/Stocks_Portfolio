using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockProfile
    {
        public List<StockInfo> StockList { get; set; }
        public int StockCount { get; set; }
        public double TotalProfit { get; set; }
        public double CurrentValue { get; set; }
        public double TotalCost { get; set; }
        public double AvgPrice { get; set; }
    }
}