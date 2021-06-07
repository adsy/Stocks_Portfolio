using MediatR;
using Services;
using Services.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Handlers
{
    public class GetStockChartDataQuery : IRequest<Response<StockChartData>>
    {
        public string Id { get; set; }
    }
}