namespace Domain.Config
{
    public class ExchangeRateStorageApiSettings : ApiSettings
    {
        public const string ConfigKey = "ApiSettings:ExchangeRateStorage";

        public const string GetExchangeRateKey = "GetExchangeRate";

        public string GetExchangeRateUri => GetEndpointUri(GetExchangeRateKey);
    }
}