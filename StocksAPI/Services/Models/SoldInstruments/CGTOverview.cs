using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.SoldInstruments
{
    public class CGTOverview
    {
        public double CapitalGains { get; set; }
        public double CapitalLosses { get; set; }
        public double CGTPayable { get; set; }
        public List<SoldInstrumentDTO> SalesList { get; set; }
    }
}