namespace Domain.Config
{
    public class YahooFinanceApiSettings : ApiSettings
    {
        public const string ConfigKey = "ApiSettings:YahooFinance";

        public const string GetQuotesKey = "GetQuotes";
        public const string GetStockSummaryKey = "GetStockSummary";
        public const string GetStockNewsKey = "GetStockNews";
        public const string GetStockChartKey = "GetStockChart";

        public const string ApiKeyHeaderKey = "x-rapidapi-key";
        public const string ApiHostHeaderKey = "x-rapidapi-host";

        public string GetQuotesUri => Endpoints[GetQuotesKey];
        public string GetStockSummaryUri => Endpoints[GetStockSummaryKey];
        public string GetStockNewUri => Endpoints[GetStockNewsKey];
        public string GetStockChartUri => Endpoints[GetStockChartKey];

        public string GetApiKeyHeader => ApiKeyHeaderKey;
        public string GetApiHostHeader => ApiHostHeaderKey;

        public string GetApiKeyHeaderValue => Headers[ApiKeyHeaderKey];
        public string GetApiHostHeaderValue => Headers[ApiHostHeaderKey];
    }
}