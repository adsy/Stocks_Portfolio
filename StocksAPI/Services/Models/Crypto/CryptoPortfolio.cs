using Services.Models.Crypto;
using System.Collections.Generic;

namespace Services.Models
{
    public class CryptoPortfolio
    {
        public CryptoPortfolio()
        {
            Cryptocurrencies = new List<CoinInfo>();
        }

        public List<CoinInfo> Cryptocurrencies { get; set; }
    }
}