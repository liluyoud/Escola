using Microsoft.AspNetCore.Mvc;
using School.Core.Entities;
using School.Core.Repositories;

namespace School.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetAllAsync()
        {
            var cities = await cityRepository.GetAllAsync();
            return Ok(cities);
        }

    }
}
