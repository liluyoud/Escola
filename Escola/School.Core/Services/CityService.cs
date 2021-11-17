using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sapiens.Core.Services;
using School.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Services
{
    public interface ICityService : IEntityService<City>
    {

    }

    public class CityService : EntityService, ICityService
    {
        public CityService(HttpClient http, IConfiguration conf, ILogger<EntityService> log) : base(http, conf, log)
        {

        }

        public async Task AddAsync(City city)
        {
            await PostJsonAsync(city);
        }

        public async Task DeleteAsync(long id)
        {
            await DeleteAsync($"{id}");
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            var cities = await GetJsonAsync<List<City>>();
            return cities.OrderBy(item => item.Name).ToList();
        }

        public async Task<City> GetByIdAsync(long id)
        {
            return await GetJsonAsync<City>($"{id}");
        }

        public async Task Update(City city)
        {
            await PutJsonAsync(city, $"{city.Id}");
        }

    }
}
