using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Config
{
    public class CoinGeckoApiSettings : ApiSettings
    {
        public const string ConfigKey = "ApiSettings:CoinGecko";

        public const string CoinChartDataUriKey = "GetCoinChartData";
        public const string CoinSummaryDataUriKey = "GetCoinSummaryData";

        public const string CoinGeckoApiKeyHeaderKey = "x-rapidapi-key";
        public const string CoinGeckoApiHostHeaderKey = "x-rapidapi-host";

        public string GetCoinChartDataUri => GetEndpointUri(CoinChartDataUriKey);
        public string GetCoinSummaryDataUri => GetEndpointUri(CoinSummaryDataUriKey);

        public string GetHostHeader => CoinGeckoApiHostHeaderKey;
        public string GetKeyHeader => CoinGeckoApiKeyHeaderKey;
        public string GetHostHeaderValue => GetEndpointHeader(CoinGeckoApiHostHeaderKey);
        public string GetKeyHeaderValue => GetEndpointHeader(CoinGeckoApiKeyHeaderKey);
    }
}