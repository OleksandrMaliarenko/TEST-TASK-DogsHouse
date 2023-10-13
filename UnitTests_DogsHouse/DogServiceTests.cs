using AutoMapper;
using BLL_DogsHouse.AutoMapper;
using BLL_DogsHouse.Models.Queries;
using BLL_DogsHouse.Models.Requests;
using BLL_DogsHouse.Models.Views;
using BLL_DogsHouse.Services;
using DAL_DogsHouse.Entities;
using DAL_DogsHouse.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests_DogsHouse
{
    public class DogServiceTests
    {
        private readonly IMapper _mapper;

        public DogServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public async Task GetDogs_EmptyDatabase_ThrowsException()
        {
            //Arrange
            IEnumerable<Dog> dogs = null;
            DogQuery dogQuery = new DogQuery();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var exception = await Assert.ThrowsAsync<Exception>(() => service.Get(dogQuery));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("DataBase is empty", exception.Message);
        }

        [Fact]
        public async Task GetDogs_InvalidOrder_ThrowsAgrumentException()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            DogQuery dogQuery = new DogQuery() { order = "Invalid" };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Get(dogQuery));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Wrong order", exception.Message);
        }

        [Fact]
        public async Task GetDogs_ValidOrder_ShouldReturnListWithCorrectOrder()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            var expectedData = initialData.OrderByDescending(x => x.Name).ToList();
            DogQuery dogQuery = new DogQuery() { order = "desc" };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var result = await service.Get(dogQuery);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData[0].Name, result.ToList()[0].Name);
            Assert.Equal(expectedData[1].Name, result.ToList()[1].Name);
            Assert.Equal(expectedData[2].Name, result.ToList()[2].Name);
        }

        [Fact]
        public async Task GetDogs_InvalidAttribute_ThrowsAgrumentException()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;     
            DogQuery dogQuery = new DogQuery() { attribute = "Invalid" };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Get(dogQuery));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Wrong attribute", exception.Message);
        }

        [Fact]
        public async Task GetDogs_ValidAttribute_ShouldReturnListWithCorrectOrderByAttribute()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            var expectedData = initialData.OrderBy(x => x.Weight).ToList();
            DogQuery dogQuery = new DogQuery() { attribute = "weight" };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var result = await service.Get(dogQuery);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData[0].Name, result.ToList()[0].Name);
            Assert.Equal(expectedData[1].Name, result.ToList()[1].Name);
            Assert.Equal(expectedData[2].Name, result.ToList()[2].Name);
        }

        [Fact]
        public async Task GetDogs_InvalidPaginationLessThan0_ThrowsAgrumentException()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            DogQuery dogQuery = new DogQuery() { pageNumber = -1 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Get(dogQuery));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Wrong pagination", exception.Message);
        }

        [Fact]
        public async Task GetDogs_InvalidPagination_ThrowsAgrumentException()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            DogQuery dogQuery = new DogQuery() { pageNumber = 4, pageSize = 10 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Get(dogQuery));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Wrong pagination", exception.Message);
        }

        [Fact]
        public async Task GetDogs_ValidPagination_ShouldReturnPaginationList()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            var expectedData = initialData.Skip(0).Take(2).ToList();
            DogQuery dogQuery = new DogQuery() { pageNumber = 1, pageSize = 2 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var result = await service.Get(dogQuery);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData[0].Name, result.ToList()[0].Name);
            Assert.Equal(expectedData[1].Name, result.ToList()[1].Name);
        }

        [Fact]
        public async Task GetDogs_ValidPaginationAndValidAttribute_ShouldReturnPaginationListOrderByAttribute()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            var expectedData = initialData.OrderBy(x => x.Color).Skip(0).Take(2).ToList();
            DogQuery dogQuery = new DogQuery() { attribute="color", pageNumber = 1, pageSize = 2 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var result = await service.Get(dogQuery);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData[0].Name, result.ToList()[0].Name);
            Assert.Equal(expectedData[1].Name, result.ToList()[1].Name);
        }

        [Fact]
        public async Task CreateDog_ValidDogRequest_ShouldReturnIdenticalModel()
        {
            //Arrange
            var entity = new Dog { Name = "ATest1", Color = "Red", Tail_Length = 10, Weight = 15 };
            var request = new DogRequest { Name = "ATest1", Color = "Red", Tail_Length = 10, Weight = 15 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.Create(It.IsAny<Dog>())).Returns(Task.FromResult(entity));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var result = await service.Create(request);

            //Assert
            Assert.IsType<DogView>(result);
            Assert.Equal(result.Name, request.Name);
            Assert.Equal(result.Color, request.Color);
            Assert.Equal(result.Tail_Length, request.Tail_Length);
            Assert.Equal(result.Weight, request.Weight);
        }

        [Fact]
        public async Task CreateDog_InValidDogRequest_ThrowsAgrumentException()
        {
            //Arrange
            IEnumerable<Dog> dogs = initialData;
            var entity = new Dog { Name = "ATest1", Color = "Red", Tail_Length = 10, Weight = 15 };
            var request = new DogRequest { Name = "ATest1", Color = "Red", Tail_Length = 10, Weight = 15 };
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.DogRepository.Create(It.IsAny<Dog>())).Returns(Task.FromResult(entity));
            mockUnitOfWork.Setup(m => m.DogRepository.GetAll()).Returns(Task.FromResult(dogs));
            var service = new DogService(mockUnitOfWork.Object, _mapper);

            //Act
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.Create(request));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Dog with this name already exist", exception.Message);
        }

        #region InitialData

        List<Dog> initialData = new List<Dog>()
        {
            new Dog(){ Name = "ATest1", Color = "Red", Tail_Length = 10, Weight = 15 },
            new Dog(){ Name = "BTest2", Color = "Blue", Tail_Length = 17, Weight = 23 },
            new Dog(){ Name = "CTest3", Color = "White", Tail_Length = 23, Weight = 42 },
        };

        #endregion

    }
}
