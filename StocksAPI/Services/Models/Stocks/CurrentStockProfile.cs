using Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class CurrentStockProfile : Stock
    {
        public CurrentStockProfile(Stock stock) : base(stock.Id, stock.Name, stock.PurchaseDate, stock.PurchasePrice, stock.Amount, stock.TotalCost, stock.Country)
        {
        }

        public double currentPrice { get; set; }
        public double currentValue { get; set; }
        public double profit { get; set; }
        public double averagePrice { get; set; }
    }
}