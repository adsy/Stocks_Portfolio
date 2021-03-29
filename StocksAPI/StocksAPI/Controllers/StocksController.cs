using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Data;
using Services.Models;
using Services.Stocks.Commands;
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

        [Authorize]
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
        [Route("GetPortfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var result = await _mediator.Send(new GetPortfolioQuery());

            return Ok(result);
        }

        [HttpPost]
        [Route("AddStock")]
        public async Task<IActionResult> AddStock([FromBody] StockDTO stockData)
        {
            try
            {
                var result = await _mediator.Send(new AddStockCommand
                {
                    stock = stockData
                });
                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error("---Error adding stock--- \n" + e.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("SellStock")]
        public async Task<IActionResult> SellStockAsync([FromBody] SellStockDTO sellStockDtoParam)
        {
            try
            {
                var result = await _mediator.Send(new SellStockCommand
                {
                    sellStockDTO = sellStockDtoParam
                });

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error("Error in SellStockFunction - " + e.Message);
                return Problem("Error in SellStockAsync function", statusCode: 500);
            }
        }
    }
}