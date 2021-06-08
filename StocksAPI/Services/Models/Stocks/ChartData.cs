using System;

namespace Services.Models.Stocks
{
    public class ChartData
    {
        public double price { get; set; }
        public long timestampLong { get; set; }
        public string time { get; set; }
    }
}