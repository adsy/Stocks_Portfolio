using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Portfolio.Query;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PortfolioController> _log;

        public PortfolioController(IMediator mediator, ILogger<PortfolioController> log)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        [Authorize]
        [HttpGet]
        [Route("GetPortfolio")]
        public async Task<IActionResult> GetPortfolio()
        {
            try
            {
                var result = await _mediator.Send(new GetPortfolioQuery());

                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError($"Error throw within GetPortfolioAzureFunction - {e.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("GetPortfolioAzureFunction")]
        public async Task<IActionResult> GetPortfolioAzureFunction()
        {
            try
            {
                var result = await _mediator.Send(new GetPortfolioQuery());

                return Ok(result);
            }
            catch (Exception e)
            {
                _log.LogError($"Error throw within GetPortfolioAzureFunction - {e.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}