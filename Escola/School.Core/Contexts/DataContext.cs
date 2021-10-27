using Microsoft.EntityFrameworkCore;

namespace School.Core.Contexts
{
    public class DataContext: DbContext
    {
        public DbSet<Entities.City>? Cities { get; set; }
        public DbSet<Entities.School>? Schools { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        } 
    }
}
