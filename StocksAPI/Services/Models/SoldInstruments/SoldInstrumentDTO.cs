using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.SoldInstruments
{
    public class SoldInstrumentDTO
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime PuchaseDate { get; set; }
        public double PurchasePrice { get; set; }
        public DateTime SaleDate { get; set; }
        public double SalePrice { get; set; }
        public double Profit { get; set; }
        public bool DiscountApplied { get; set; }
        public double CGTPayable { get; set; }
    }
}