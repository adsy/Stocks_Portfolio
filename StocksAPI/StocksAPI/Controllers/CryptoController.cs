using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Crypto.Commands;
using Services.Crypto.Querys;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockAPI.Controllers
{
    public class CryptoController : Controller
    {
        private readonly IMediator _mediator;

        public CryptoController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [Route("GetValues/{ids}")]
        [HttpGet]
        public async Task<IActionResult> GetValuesAsync(string ids)
        {
            try
            {
                var result = await _mediator.Send(new GetCryptoValuesAsyncQuery
                {
                    Ids = ids
                });

                if (result != null) return Ok(result);

                return BadRequest("One of the symbols you entered was incorrect");
            }
            catch (Exception e)
            {
                Log.Error("---Error getting crypto values--- \n" + e.Message);
                return BadRequest();
            }
        }

        [Route("AddCrypto")]
        [HttpPost]
        public async Task<IActionResult> AddCryptoToDbAsync([FromBody] CryptocurrencyDTO crypto)
        {
            try
            {
                var result = await _mediator.Send(new AddCryptoToDbAsyncCommand
                {
                    crypto = crypto
                });

                if (result.StatusCode == (int)HttpStatusCode.OK) return Ok();

                return BadRequest("The crpytocurrency you added had errors in the data.");
            }
            catch (Exception e)
            {
                Log.Error("---Error adding crypto to do --- \n" + e.Message);
                return Problem();
            }
        }

        [Route("GetCryptoPortfolio")]
        [HttpGet]
        public async Task<IActionResult> GetCryptoPortfolioAsync()
        {
            try
            {
                var result = await _mediator.Send(new GetCryptoPortfolioAsyncQuery());

                if (result != null) return Ok(result);

                return BadRequest();
            }
            catch (Exception e)
            {
                Log.Error("--- Error getting crypto portfolio --- \n" + e.Message);
                return Problem();
            }
        }
    }
}