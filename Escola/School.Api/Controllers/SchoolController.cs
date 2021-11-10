using Microsoft.AspNetCore.Mvc;
using School.Core.Repositories;

namespace School.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolRepository schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok("School");
        }

    }
}
