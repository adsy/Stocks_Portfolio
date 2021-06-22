using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoMarketData
    {
        public Dictionary<string, string> High_24h { get; set; }
        public Dictionary<string, string> Low_24h { get; set; }
        public string Price_Change_Percentage_24h { get; set; }
        public string Market_Cap_Change_24h { get; set; }
        public string Market_Cap_Change_Percentage_24h { get; set; }
    }
}