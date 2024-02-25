
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAPI.Models
{
    public class DemoDbContext : DbContext
    {
        public DbSet<License> Licenses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options)
        {

        }

    }
}
