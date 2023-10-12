using BLL_DogsHouse.Models.Queries;
using DAL_DogsHouse.Entities;
using DAL_DogsHouse.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IEnumerable<Dog>> Get(DogQuery dogQuery)
        {
            return await _context.Dogs.Skip(dogQuery.Offset).Take(dogQuery.Limit).ToListAsync();
        }
        public async Task<Dog> Create(Dog dog)
        {
            var dogEntity = await _context.AddAsync(dog);

            return dogEntity.Entity;
        }
    }
}
