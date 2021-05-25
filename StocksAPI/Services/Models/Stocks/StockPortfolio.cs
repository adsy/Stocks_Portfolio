using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockPortfolio
    {
        public StockPortfolio()
        {
            PortfolioProfit = new PortfolioProfit();
            CurrentStockPortfolio = new Dictionary<string, StockProfile>();
        }

        public PortfolioProfit PortfolioProfit { get; set; }
        public Dictionary<string, StockProfile> CurrentStockPortfolio { get; set; }
    }
}