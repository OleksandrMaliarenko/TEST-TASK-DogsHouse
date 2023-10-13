using BLL_DogsHouse.Models.Queries;
using BLL_DogsHouse.Models.Requests;
using BLL_DogsHouse.Models.Views;

namespace BLL_DogsHouse.Interfaces
{
    public interface IDogService
    {
        Task<IEnumerable<DogView>> Get(DogQuery dogQuery);
        Task<DogView> Create(DogRequest dogRequest);
    }
}
