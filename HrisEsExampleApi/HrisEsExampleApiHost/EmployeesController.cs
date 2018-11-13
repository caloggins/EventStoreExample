using System;
using System.Threading.Tasks;
using EmployeeBenefitsLibrary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrisEsExampleApiHost
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Hire([FromBody]HireEmployee request)
        {
            try
            {
                await mediator.Send(request);

                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPost("{id:guid}/salary")]
        public async Task<IActionResult> SetSalary(Guid id, [FromBody]ChangeSalary request)
        {
            request.EmployeeId = id;

            await mediator.Send(request);

            return Ok();
        }

        [HttpPost("{id:guid}/terminate")]
        public async Task<IActionResult> Terminate(Guid id, Terminate request)
        {
            request.EmployeeId = id;

            await mediator.Send(request);

            return Ok();
        }
    }
}