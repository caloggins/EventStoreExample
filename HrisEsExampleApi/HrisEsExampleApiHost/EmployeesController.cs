using System;
using System.Threading.Tasks;
using EmployeeBenefitsLibrary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrisEsExampleApiHost
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator mediator;

        public EmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public ActionResult<string> Employees()
        {
            return "Hello, world.";
        }

        [HttpPost("hire")]
        public async Task<IActionResult> Create([FromBody]HireEmployee details)
        {
            try
            {
                await mediator.Send(details);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }
    }
}