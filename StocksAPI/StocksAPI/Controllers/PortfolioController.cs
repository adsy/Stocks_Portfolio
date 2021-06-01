﻿using MediatR;
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
    public class PortfolioController : Controller
    {
        private readonly IMediator _mediator;

        public PortfolioController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("GetPortfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            var result = await _mediator.Send(new GetPortfolioQuery());

            return Ok(result);
        }
    }
}