using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Stocks
{
    public class StockNews
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public string Publisher { get; set; }
        public string Type { get; set; }
        public string PublishedAt { get; set; }
    }
}