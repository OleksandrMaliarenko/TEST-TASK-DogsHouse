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
        Task<IEnumerable<Dog>> GetAll();
        Task<Dog> Create(Dog dog);
    }
}
