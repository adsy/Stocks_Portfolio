using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config
{
    public class CoinMarketCapApiSettings : ApiSettings
    {
        public const string ConfigKey = "ApiSettings:CoinMarketCap";

        public const string CoinQuoteDataUriKey = "GetCoinQuoteData";
        public string GetCoinQuoteDataUri => Endpoints[CoinQuoteDataUriKey];

        public string KeyHeaderKey = "X-CMC_PRO_API_KEY";
        public string GetKeyHeaderKey => KeyHeaderKey;
        public string GetKeyHeaderValue => Headers[KeyHeaderKey];
    }
}