using Services.Models.Crypto;
using System.Collections.Generic;

namespace Services.Models
{
    public class CryptoPortfolio
    {
        public CryptoPortfolio()
        {
            Cryptocurrencies = new Dictionary<string, CryptoProfile>();
        }

        public Dictionary<string, CryptoProfile> Cryptocurrencies { get; set; }
    }
}