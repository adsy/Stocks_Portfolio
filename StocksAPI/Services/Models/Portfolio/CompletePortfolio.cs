using Services.Models.Crypto;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Portfolio
{
    public class CompletePortfolio
    {
        public CompletePortfolio()
        {
            PortfolioProfit = new PortfolioProfit();
            CurrentStockPortfolio = new StockPortfolio();
            CurrentCryptoPortfolio = new CryptoPortfolio();
        }

        public PortfolioProfit PortfolioProfit { get; set; }
        public StockPortfolio CurrentStockPortfolio { get; set; }
        public CryptoPortfolio CurrentCryptoPortfolio { get; set; }
    }
}