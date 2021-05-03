using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class PortfolioTrackerDTO
    {
        public double PortfolioTotal { get; set; }
        public DateTime TimeStamp { get; set; }
        public string timeString { get; set; }
    }
}