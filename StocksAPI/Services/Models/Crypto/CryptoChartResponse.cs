using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoChartResponse
    {
        public string[][] Prices { get; set; }
        public string[][] Market_Caps { get; set; }
        public string[][] Total_Volumes { get; set; }
    }
}
