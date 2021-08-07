using System;
using System.ComponentModel.DataAnnotations;

namespace Services.Data
{
    public class SoldInstrument
    {
        [Key]
        public int Id { get; set; }

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