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
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of locations to be assigned to assets
        /// </summary>
        /// <returns>A list of <seealso cref="Location"/></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(LocationList))]
        public async Task<IActionResult> GetLocations()
        {
            return Ok(await _mediator.Send(new GetLocationsRequest()));
        }
    }
}