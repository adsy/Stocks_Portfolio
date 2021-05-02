using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{
    public class PortfolioTracker
    {
        public int Id { get; set; }
        public double PortfolioTotal { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}