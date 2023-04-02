using Application.Features.Cities.Commands.Add;
using Application.Features.Cities.Queries.GetAll;
using Application.Features.Excel.Commands.ReadCities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{ 
    public class CitiesController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync(CityAddCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<IActionResult> GetAllAsync(CityGetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet( "{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok(await Mediator.Send(new CityGetAllQuery(id, null)));
        }
    }
}
