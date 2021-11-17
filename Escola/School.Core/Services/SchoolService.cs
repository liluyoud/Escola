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
    public interface ISchoolService : IEntityService<Entities.School>
    {

    }

    public class SchoolService : EntityService, ISchoolService
    {
        public SchoolService(HttpClient http, IConfiguration conf, ILogger<EntityService> log) : base(http, conf, log)
        {

        }

        public async Task AddAsync(Entities.School school)
        {
            await PostJsonAsync(school);
        }

        public async Task DeleteAsync(long id)
        {
            await DeleteAsync($"{id}");
        }

        public async Task<IEnumerable<Entities.School>> GetAllAsync()
        {
            var schools = await GetJsonAsync<List<Entities.School>>();
            return schools.OrderBy(item => item.Name).ToList();
        }

        public async Task<Entities.School> GetByIdAsync(long id)
        {
            return await GetJsonAsync<Entities.School>($"{id}");
        }

        public async Task Update(Entities.School school)
        {
            await PutJsonAsync(school, $"{school.Id}");
        }

    }
}
