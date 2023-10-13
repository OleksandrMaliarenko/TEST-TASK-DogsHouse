using DAL_DogsHouse.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL_DogsHouse
{
    public class DogsHouseDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DogsHouseDbContext(DbContextOptions<DogsHouseDbContext> options) : base(options) { }
    }
}
