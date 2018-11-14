using System;
using System.Threading.Tasks;
using EmployeeBenefitsLibrary;
using EmployeeDataLibrary;
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
        public async Task<IActionResult> Employees()
        {
            var request = new GetEmployees();

            var result = await mediator.Send(request);

            return Ok(result);
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

        [HttpGet("{id:guid}/salary")]
        public async Task<IActionResult> GetSalary(Guid id)
        {
            var request = new GetSalaryForEmployee { EmployeeId = id };

            var result = await mediator.Send(request);

            return Ok(result);
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