using AutoMapper;
using BLL_DogsHouse.Models.Requests;
using BLL_DogsHouse.Models.Views;
using DAL_DogsHouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
