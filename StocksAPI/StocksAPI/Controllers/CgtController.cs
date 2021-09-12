using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Cgt.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/CGT/")]
    public class CgtController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CgtController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("GetCgtData")]
        public async Task<IActionResult> GetCgtDataAsync()
        {
            var result = await _mediator.Send(new GetCgtDataQuery());

            if (result.StatusCode == (int)HttpStatusCode.OK)
                return Ok(result.Data);

            return StatusCode(result.StatusCode, result.Message);
        }
    }
}