using MeuProjetoCrudAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MeuProjetoCrudAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
    }
}
