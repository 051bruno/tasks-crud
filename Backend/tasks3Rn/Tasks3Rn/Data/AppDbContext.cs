using Microsoft.EntityFrameworkCore;
using Tasks3Rn.Models;

namespace MeuProjetoCrudAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
