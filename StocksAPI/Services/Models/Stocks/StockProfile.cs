using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockProfile
    {
        public StockProfile()
        {
            StockCount = 0;
            AvgPrice = 0;
            TotalProfit = 0;
            TotalCost = 0;
            CurrentValue = 0;
            TotalAmount = 0;
            CurrentPrice = 0;
        }

        public List<StockInfo> StockList { get; set; }
        public int StockCount { get; set; }
        public double TotalProfit { get; set; }
        public double CurrentValue { get; set; }
        public double TotalCost { get; set; }
        public double AvgPrice { get; set; }
        public double TotalAmount { get; set; }
        public double CurrentPrice { get; set; }
    }
}