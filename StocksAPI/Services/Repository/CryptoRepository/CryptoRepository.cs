using AutoMapper;
using Newtonsoft.Json.Linq;
using Services.Data;
using Services.Interfaces.Repository;
using Services.IRepository;
using Services.Models;
using Services.Models.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.CryptoRepository
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CryptoRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Response> AddCryptoToDbAsync(CryptocurrencyDTO crypto)
        {
            var fnResult = new Response
            {
                StatusCode = 200,
                Error = false
            };

            var cryptoObj = _mapper.Map<Cryptocurrency>(crypto);

            await _unitOfWork.Cryptocurrencies.Insert(cryptoObj);

            await _unitOfWork.Save();

            return fnResult;
        }

        public async Task<CryptoPortfolio> GetCryptoPortfolioAsync()
        {
            var cryptocurrenies = await _unitOfWork.Cryptocurrencies.GetAll();

            var cryptoPortfolio = new CryptoPortfolio();

            var queryString = "";

            foreach (var crypto in cryptocurrenies)
            {
                var coinInfo = _mapper.Map<CoinInfo>(crypto);

                cryptoPortfolio.Cryptocurrencies.Add(coinInfo);

                queryString += coinInfo.Name + ",";
            }

            var ids = queryString.Remove(queryString.Length - 1);

            var prices = await GetCryptoValuesAsync(ids);

            foreach (var value in prices)
            {
                var result = cryptoPortfolio.Cryptocurrencies.Find(q => q.Name == value.Name);

                result.CurrentPrice = value.Price;

                result.CurrentValue = result.Amount * result.CurrentPrice;

                result.Profit = result.CurrentValue - result.TotalCost;
            }

            return cryptoPortfolio;
        }

        public async Task<IEnumerable<CryptoValue>> GetCryptoValuesAsync(string ids)
        {
            var cryptoTickers = ids.Split(",");

            var apiUrl = $"https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol={ids}&convert=AUD";

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
                    Price = (double)requestBody.SelectToken($"data.{ticker}.quote.AUD.price")
                };
                cryptoValueList.Add(cryptoTicker);
            }

            return cryptoValueList;
        }
    }
}