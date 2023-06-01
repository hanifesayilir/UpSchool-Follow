using Application.Features.Accounts.Queries.GetAll;
using Application.Features.Accounts.Queries.GetById;
using Application.Features.Cities.Commands.Add;
using Application.Features.Cities.Queries.GetAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageNumber=1, int pageSize=10)
        {
            return Ok(await Mediator.Send(new AccountGetAllQuery(pageNumber, pageSize)));
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(await Mediator.Send(new AccountGetByIdQuery(id)));
        }
    }
}
