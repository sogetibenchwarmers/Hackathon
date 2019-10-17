using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackathonApi.Mediator;
using HackathonApi.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HackathonApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SubLocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(SubLocationList))]
        public async Task<IActionResult> GetLocations()
        {
            return Ok(await _mediator.Send(new GetSubLocationsRequest()));
        }
    }
}