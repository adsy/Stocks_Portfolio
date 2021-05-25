using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class PortfolioProfit
    {
        public PortfolioProfit()
        {
            CurrentTotal = 0;
            PurchaseTotal = 0;
        }

        public double CurrentTotal { get; set; }
        public double PurchaseTotal { get; set; }
    }
}