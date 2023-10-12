using BLL_DogsHouse.Models.Queries;
using DAL_DogsHouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DogsHouse.Interfaces
{
    public interface IDogRepository
    {
        public Task<IEnumerable<Dog>> GetAll();
        public Task<IEnumerable<Dog>> Get(DogQuery dogQuery);
        public Task<Dog> Create(Dog dog);
    }
}
