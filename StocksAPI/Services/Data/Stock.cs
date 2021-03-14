using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
    public class Stock
    {
        public Stock(int id, string name, DateTime purchaseDate, double purchasePrice, double amount, double totalCost, string country)
        {
            Id = id;
            Name = name;
            PurchaseDate = purchaseDate;
            PurchasePrice = purchasePrice;
            Amount = amount;
            TotalCost = totalCost;
            Country = country;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PurchasePrice { get; set; }
        public double Amount { get; set; }
        public double TotalCost { get; set; }
        public string Country { get; set; }
    }
}