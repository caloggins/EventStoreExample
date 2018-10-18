using Microsoft.AspNetCore.Mvc;

namespace HrisEsExampleApiHost
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
         [HttpGet]
        public ActionResult<string> Employees()
         {
             return "Hello, world.";
         }
    }

    public class Employee
    {
        public string Name => "Chris";
    }
}