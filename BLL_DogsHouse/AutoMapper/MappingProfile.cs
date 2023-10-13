using AutoMapper;
using BLL_DogsHouse.Models.Requests;
using BLL_DogsHouse.Models.Views;
using DAL_DogsHouse.Entities;

namespace BLL_DogsHouse.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dog, DogView>();
            CreateMap<DogRequest, Dog>();
        }
    }
}
