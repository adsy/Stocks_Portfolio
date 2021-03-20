using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class Portfolio
    {
        public PortfolioProfit _PortfolioProfit { get; set; }
        public List<CurrentStockProfile> _CurrentStockPortfolio { get; set; }
    }
}