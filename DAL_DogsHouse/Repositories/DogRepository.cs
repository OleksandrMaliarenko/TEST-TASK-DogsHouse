using DAL_DogsHouse.Entities;
using DAL_DogsHouse.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL_DogsHouse.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly DogsHouseDbContext _context;

        public DogRepository(DogsHouseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dog>> GetAll()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<Dog> Create(Dog dog)
        {
            var dogEntity = await _context.AddAsync(dog);

            return dogEntity.Entity;
        }
    }
}
