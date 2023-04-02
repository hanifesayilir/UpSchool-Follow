using Application.Features.Excel.Commands.ReadCities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    
    [HttpPost]
    public async Task<IActionResult> ReadCitiesAsync(ExcelReadCitiesCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
