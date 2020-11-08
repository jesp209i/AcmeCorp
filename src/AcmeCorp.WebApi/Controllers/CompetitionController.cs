using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeCorp.Application.Commands;
using AcmeCorp.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AcmeCorp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompetitionController : Controller
    {
        private readonly IMediator _mediator;

        public CompetitionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> EnterCompetition(EnterCompetition request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _mediator.Send(new GetProducts()));
        }
    }
}
