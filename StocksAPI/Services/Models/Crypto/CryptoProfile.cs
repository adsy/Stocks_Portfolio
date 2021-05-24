using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoProfile
    {
        public List<CoinInfo> CoinList { get; set; }
        public int CoinCount { get; set; }
        public double TotalProfit { get; set; }
        public double AvgPrice { get; set; }
    }
}