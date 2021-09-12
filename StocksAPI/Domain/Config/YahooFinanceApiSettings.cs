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

        public string GetQuotesUri => GetEndpointUri(GetQuotesKey);
        public string GetStockSummaryUri => GetEndpointUri(GetStockSummaryKey);
        public string GetStockNewUri => GetEndpointUri(GetStockNewsKey);
        public string GetStockChartUri => GetEndpointUri(GetStockChartKey);

        public string GetApiKeyHeader => ApiKeyHeaderKey;
        public string GetApiHostHeader => ApiHostHeaderKey;

        public string GetApiKeyHeaderValue => GetEndpointHeader(ApiKeyHeaderKey);
        public string GetApiHostHeaderValue => GetEndpointHeader(ApiHostHeaderKey);
    }
}