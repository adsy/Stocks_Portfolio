using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PortfolioTrackerFunction.Infrastructure.Interfaces;
using PortfolioTrackerFunction.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioTrackerFunction.Infrastructure.Repository
{
    public class StocksApiRepository : IStocksApiRepository
    {
        private readonly ILogger<IStocksApiRepository> _log;
        private readonly HttpClient _client;

        public StocksApiRepository(HttpClient client, ILogger<IStocksApiRepository> log)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<ServiceProcessResult> AddValueToDB(double value)
        {
            var fnResult = new ServiceProcessResult<double>
            {
                ServiceResultCode = (int)HttpStatusCode.BadRequest
            };

            var portfolio = new PortfolioTracker { PortfolioTotal = value, TimeStamp = DateTime.Now };

            var json = JsonConvert.SerializeObject(portfolio);

            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("https://stockapi20210415184956.azurewebsites.net/api/Stocks/AddPortfolioValue", data);

            if ((int)response.StatusCode == (int)HttpStatusCode.OK)
            {
                fnResult.ServiceResultCode = (int)response.StatusCode;

                return fnResult;
            }

            return fnResult;
        }

        public async Task<ServiceProcessResult<double>> GetPortfolioValue()
        {
            var fnResult = new ServiceProcessResult<double>
            {
                ServiceResultCode = (int)HttpStatusCode.BadRequest
            };

            try
            {
                HttpResponseMessage response = await _client.GetAsync("https://stockapi20210415184956.azurewebsites.net/api/stocks/GetPortfolio");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var portfolioProfit = JsonConvert.DeserializeObject<Portfolio>(responseBody);

                fnResult.Data = portfolioProfit._PortfolioProfit.CurrentTotal;

                fnResult.ServiceResultCode = (int)HttpStatusCode.OK;

                return fnResult;
            }
            catch (HttpRequestException e)
            {
                fnResult.ServiceResultCode = (int)HttpStatusCode.InternalServerError;
                fnResult.ServiceResultMessage = e.Message;
                return fnResult;
            }
        }
    }
}