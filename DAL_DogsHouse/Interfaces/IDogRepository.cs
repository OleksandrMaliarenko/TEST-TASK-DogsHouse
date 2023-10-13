using DAL_DogsHouse.Entities;

namespace DAL_DogsHouse.Interfaces
{
    public interface IDogRepository
    {
        Task<IEnumerable<Dog>> GetAll();
        Task<Dog> Create(Dog dog);
    }
}
