using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models.Crypto
{
    public class CryptoProfile
    {
        public CryptoProfile()
        {
            CoinCount = 0;
            TotalProfit = 0;
            CurrentValue = 0;
            TotalCost = 0;
            AvgPrice = 0;
            TotalAmount = 0;
            CurrentPrice = 0;
        }

        public string CoinName { get; set; }
        public List<CoinInfo> CoinList { get; set; }
        public int CoinCount { get; set; }
        public double TotalProfit { get; set; }
        public double CurrentValue { get; set; }
        public double TotalCost { get; set; }
        public double AvgPrice { get; set; }
        public double TotalAmount { get; set; }
        public double CurrentPrice { get; set; }
    }
}