using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class StockDTO
    {
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double PurchasePrice { get; set; }
        public double Amount { get; set; }
        public double TotalCost { get; set; }
        public string Country { get; set; }
    }
}