using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Stocks.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StocksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //get certain stock
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetStockDataAsync(string id)
        {
            var result = await _mediator.Send(new GetStockDataQuery
            {
                Id = id
            });

            return Ok(result);
        }

        [HttpGet]
        [Route("GetPortfolioProfit")]
        public async Task<IActionResult> GetPortfolioProfitAsync()
        {
            var result = await _mediator.Send(new GetPortfolioProfitQuery());

            return Ok(result);
        }
    }
}