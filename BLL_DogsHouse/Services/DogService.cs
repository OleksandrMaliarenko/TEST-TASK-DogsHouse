using AutoMapper;
using BLL_DogsHouse.Interfaces;
using BLL_DogsHouse.Models.Queries;
using BLL_DogsHouse.Models.Requests;
using BLL_DogsHouse.Models.Views;
using DAL_DogsHouse.Entities;
using DAL_DogsHouse.Interfaces;

namespace BLL_DogsHouse.Services
{
    public class DogService : IDogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DogView>> Get(DogQuery dogQuery)
        {
            dogQuery.attribute = dogQuery.attribute.ToLower();
            dogQuery.order = dogQuery.order.ToLower();

            if (dogQuery.order != "desc" && dogQuery.order != "asc")
            {
                throw new ArgumentException("Wrong order");
            }

            if (dogQuery.attribute != "name" && dogQuery.attribute != "color" && dogQuery.attribute != "tail_length" && dogQuery.attribute != "weight")
            {
                throw new ArgumentException("Wrong attribute");
            }

            if (dogQuery.pageSize <= 0 || dogQuery.pageNumber <= 0)
            {
                throw new ArgumentException("Wrong pagination");
            }

            var dogs = await _unitOfWork.DogRepository.GetAll();

            if (dogs == null)
            {
                throw new Exception("DataBase is empty");
            }

            if (dogQuery.pageNumber > 1 && (dogQuery.pageNumber - 1) * dogQuery.pageSize > dogs.Count() - 1)
            {
                throw new ArgumentException("Wrong pagination");
            }

            if (dogQuery.attribute == "name")
            {
                if (dogQuery.order == "asc")
                {
                    dogs = dogs.OrderBy(x => x.Name);
                }
                else
                {
                    dogs = dogs.OrderByDescending(x => x.Name);
                }
            }
            else
            {
                if (dogQuery.attribute == "color")
                {
                    if (dogQuery.order == "asc")
                    {
                        dogs = dogs.OrderBy(x => x.Color);
                    }
                    else
                    {
                        dogs = dogs.OrderByDescending(x => x.Color);
                    }
                }
                else
                {
                    if (dogQuery.attribute == "tail_length")
                    {
                        if (dogQuery.order == "asc")
                        {
                            dogs = dogs.OrderBy(x => x.Tail_Length);
                        }
                        else
                        {
                            dogs = dogs.OrderByDescending(x => x.Tail_Length);
                        }
                    }
                    else
                    {
                        if (dogQuery.attribute == "weight")
                        {
                            if (dogQuery.order == "asc")
                            {
                                dogs = dogs.OrderBy(x => x.Weight);
                            }
                            else
                            {
                                dogs = dogs.OrderByDescending(x => x.Weight);
                            }
                        }
                    }
                }
            }

            if (dogQuery.pageNumber == 1)
            {
                dogs = dogs.Skip(0).Take(dogQuery.pageSize);
            }
            else
            {
                dogs = dogs.Skip((dogQuery.pageNumber - 1) * dogQuery.pageSize).Take(dogQuery.pageSize);
            }

            return _mapper.Map<IEnumerable<DogView>>(dogs);
        }

        public async Task<DogView> Create(DogRequest dogRequest)
        {
            var dogs = await _unitOfWork.DogRepository.GetAll();

            var existingDog = dogs.FirstOrDefault(x => x.Name.ToLower() == dogRequest.Name.ToLower());

            if (existingDog != null)
            {
                throw new ArgumentException("Dog with this name already exist");
            }

            var dog = await _unitOfWork.DogRepository.Create(_mapper.Map<Dog>(dogRequest));

            await _unitOfWork.SaveAsync();

            return _mapper.Map<DogView>(dog);
        }
    }
}
