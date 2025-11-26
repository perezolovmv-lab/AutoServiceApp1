using AutoServiceApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<RepairRequest> RepairRequests { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
