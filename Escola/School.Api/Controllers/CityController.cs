using Microsoft.AspNetCore.Mvc;

namespace School.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("City");
        }

    }
}
