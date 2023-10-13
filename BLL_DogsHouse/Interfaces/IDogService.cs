using BLL_DogsHouse.Models.Queries;
using BLL_DogsHouse.Models.Requests;
using BLL_DogsHouse.Models.Views;
using DAL_DogsHouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_DogsHouse.Interfaces
{
    public interface IDogService
    {
        Task<IEnumerable<DogView>> Get(DogQuery dogQuery);
        Task<DogView> Create(DogRequest dogRequest);
    }
}
