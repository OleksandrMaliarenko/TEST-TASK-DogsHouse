using DAL_DogsHouse.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DogsHouse
{
    public class DogsHouseDbContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DogsHouseDbContext(DbContextOptions<DogsHouseDbContext> options) : base(options) { }
    }
}
