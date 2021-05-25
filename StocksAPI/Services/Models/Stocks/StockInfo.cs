using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockInfo
    {
        public string Name { get; set; }
        public double PurchasePrice { get; set; }
        public double Amount { get; set; }
        public double TotalCost { get; set; }
        public double CurrentPrice { get; set; }
        public double CurrentValue { get; set; }
        public double Profit { get; set; }
    }
}