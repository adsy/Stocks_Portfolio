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
            Stocks = new Dictionary<string, StockProfile>();
        }

        public Dictionary<string, StockProfile> Stocks { get; set; }
    }
}