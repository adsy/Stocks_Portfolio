using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Crypto.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}