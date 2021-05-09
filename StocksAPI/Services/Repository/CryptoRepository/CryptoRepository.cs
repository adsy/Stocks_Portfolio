using Newtonsoft.Json.Linq;
using Services.Interfaces.Repository;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.CryptoRepository
{
    public class CryptoRepository : ICryptoRepository
    {
        public CryptoRepository()
        {
        }

        public async Task<IEnumerable<CryptoValue>> GetCryptoValuesAsync(string ids)
        {
            var cryptoTickers = ids.Split(",");

            var apiUrl = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={ids}";

            var headers = new Dictionary<string, string>
            {
                { "X-CMC_PRO_API_KEY", "a9c6f35a-153a-4123-8709-7782164e2e2b" }
            };

            var response = await HttpRequest.SendGetCall(apiUrl, headers);

            var requestBody = JObject.Parse(response);

            var cryptoValueList = new List<CryptoValue>();

            foreach (var ticker in cryptoTickers)
            {
                var cryptoTicker = new CryptoValue
                {
                    Name = ticker,
                    Price = (double)requestBody.SelectToken($"data.{ticker}.quote.USD.price")
                };
                cryptoValueList.Add(cryptoTicker);
            }

            return cryptoValueList;
        }
    }
}