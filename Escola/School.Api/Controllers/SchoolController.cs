using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("School");
        }

    }
}
