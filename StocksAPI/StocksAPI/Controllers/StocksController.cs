﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Models;
using Services.Stocks.Commands;
using Services.Stocks.Queries;
using StockAPI.Handlers;
using System;
using System.Net;
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

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok(result);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("stock-summary/{id}")]
        public async Task<IActionResult> GetStockSummaryDataAsync(string id)
        {
            var result = await _mediator.Send(new GetStockSummaryDataQuery
            {
                Id = id
            });

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok(result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("chart/{id}")]
        public async Task<IActionResult> GetStockChartDataAsync(string id)
        {
            var result = await _mediator.Send(new GetStockChartDataQuery
            {
                Id = id
            });

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok(result);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("news/{id}")]
        public async Task<IActionResult> GetStockNewsAsync(string id)
        {
            var result = await _mediator.Send(new GetStockNewsQuery
            {
                Id = id
            });

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok(result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }

        [HttpGet]
        [Route("stockPortfolio")]
        public async Task<IActionResult> GetStockPortfolioAsync()
        {
            var result = await _mediator.Send(new GetStockPortfolioQuery());

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok(result.Data);
            else
                return StatusCode(result.StatusCode, result.exception);
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

                if (result == null)
                    return BadRequest();

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error("Error in SellStock function - " + e.Message);
                return Problem("Error in SellStockAsync function", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("AddPortfolioValue")]
        public async Task<IActionResult> AddPortfolioValueAsync([FromBody] PortfolioTrackerDTO portfolioTrackerDTO)
        {
            try
            {
                var result = await _mediator.Send(new AddPortfolioValueCommand
                {
                    portfolioTracker = portfolioTrackerDTO
                });

                if (result == null)
                    return BadRequest();

                return Ok();
            }
            catch (Exception e)
            {
                Log.Error("Error in AddPortfolioValue function - " + e.Message);
                return Problem("Error in SellStockAsync function", statusCode: 500);
            }
        }

        [HttpGet]
        [Route("GetPortfolioValueList")]
        public async Task<IActionResult> GetPortfolioValueListAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetPortfolioValueListQuery());

                if (result == null)
                    return BadRequest();

                return Ok(result);
            }
            catch (Exception e)
            {
                Log.Error("Error in GetPortfolioValueList function - " + e.Message);
                return Problem("Error in SellStockAsync function", statusCode: 500);
            }
        }
    }
}