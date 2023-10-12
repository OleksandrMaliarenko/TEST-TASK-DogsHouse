using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DogsHouse.Interfaces
{
    public interface IUnitOfWork
    {
        IDogRepository DogRepository { get; }
        Task<int> SaveAsync();
    }
}
