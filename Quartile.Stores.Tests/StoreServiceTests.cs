using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Quartile.Stores.Domain.Dtos;
using Quartile.Stores.Domain.Dtos.Endpoints.Store;
using Quartile.Stores.Domain.Interfaces.Repositories;
using Quartile.Stores.Domain.Models;
using Quartile.Stores.Infra.Context;
using Quartile.Stores.Service.Services;

namespace Quartile.Stores.Tests
{
    public class StoreServiceTests
    {
        private readonly Mock<IStoreRepository> _storeRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<StoresContext> _contextMock;
        private readonly StoreService _storeService;

        public StoreServiceTests()
        {
            _storeRepositoryMock = new Mock<IStoreRepository>();
            _mapperMock = new Mock<IMapper>();
            _contextMock = new Mock<StoresContext>(new DbContextOptions<StoresContext>());

            _storeService = new StoreService(
                _storeRepositoryMock.Object,
                new Mock<ILogger<StoreService>>().Object,
                _mapperMock.Object,
                _contextMock.Object);
        }

        [Fact]
        public void GetById_ReturnsStore_WhenStoreExists()
        {
            // Arrange
            int storeId = 1;
            var storeEntity = new StoreModel { Id = storeId, Name = "Store 1" };
            var storeDto = new StoreDto { Id = storeId, Name = "Store 1" };

            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns(storeEntity);
            _mapperMock.Setup(mapper => mapper.Map<StoreDto>(storeEntity)).Returns(storeDto);

            // Act
            var result = _storeService.GetById(storeId);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(storeDto, result.Data);
        }

        [Fact]
        public void GetById_ReturnsFailure_WhenStoreNotFound()
        {
            // Arrange
            int storeId = 1;
            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns((StoreModel?)null);

            // Act
            var result = _storeService.GetById(storeId);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("Store not found.", result.Message);
        }

        [Fact]
        public void GetById_ReturnsFailure_WhenExceptionIsThrown()
        {
            // Arrange
            int storeId = 1;
            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Throws(new Exception());

            // Act
            var result = _storeService.GetById(storeId);

            // Assert
            Assert.False(result.Success);
            Assert.Null(result.Data);
            Assert.Equal("An error ocurred while trying to get a store.", result.Message);
        }

        [Fact]
        public void Create_ReturnsSuccess_WhenStoreCreated()
        {
            // Arrange
            var createRequest = new CreateStoreRequest
            {
                Name = "New Store",
                CompanyId = 1
            };
            var storeEntity = new StoreModel { Id = 1, Name = "New Store", CompanyId = 1 };

            _mapperMock.Setup(mapper => mapper.Map<StoreModel>(createRequest)).Returns(storeEntity);
            _storeRepositoryMock.Setup(repo => repo.Add(storeEntity));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Act
            var result = _storeService.Create(createRequest);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Message);
            _storeRepositoryMock.Verify(repo => repo.Add(storeEntity), Times.Once);
            _contextMock.Verify(context => context.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Create_ReturnsFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var createRequest = new CreateStoreRequest
            {
                Name = "New Store",
                CompanyId = 1
            };
            _mapperMock.Setup(mapper => mapper.Map<StoreModel>(createRequest)).Throws(new Exception());

            // Act
            var result = _storeService.Create(createRequest);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("An error ocurred while trying to create a store.", result.Message);
        }

        [Fact]
        public void Update_ReturnsSuccess_WhenStoreUpdated()
        {
            // Arrange
            int storeId = 1;
            var storeRequest = new CreateStoreRequest { Name = "Updated Store", CompanyId = 1 };
            var existingStore = new StoreModel { Name = "Old Store", CompanyId = 1 };

            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns(existingStore);
            _mapperMock.Setup(mapper => mapper.Map(storeRequest, existingStore)).Returns(existingStore);
            _storeRepositoryMock.Setup(repo => repo.Update(existingStore));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Act
            var result = _storeService.Update(storeId, storeRequest);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Message);
            _storeRepositoryMock.Verify(repo => repo.Update(existingStore), Times.Once);
            _contextMock.Verify(context => context.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_ReturnsFailure_WhenStoreNotFound()
        {
            // Arrange
            int storeId = 1;
            var storeRequest = new CreateStoreRequest { Name = "Updated Store", CompanyId = 1 };
            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns((StoreModel?)null);

            // Act
            var result = _storeService.Update(storeId, storeRequest);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Store not found.", result.Message);
        }

        [Fact]
        public void Update_ReturnsFailure_WhenExceptionIsThrown()
        {
            // Arrange
            int storeId = 1;
            var storeRequest = new CreateStoreRequest { Name = "Updated Store", CompanyId = 1 };
            var existingStore = new StoreModel { Name = "Old Store", CompanyId = 1 };

            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns(existingStore);
            _mapperMock.Setup(mapper => mapper.Map(storeRequest, existingStore)).Throws(new Exception());

            // Act
            var result = _storeService.Update(storeId, storeRequest);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("An error ocurred while trying to update a store.", result.Message);
        }

        [Fact]
        public void Delete_ReturnsSuccess_WhenStoreDeleted()
        {
            // Arrange
            int storeId = 1;
            var existingStore = new StoreModel { Id = storeId, Name = "Store to Delete" };

            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns(existingStore);
            _storeRepositoryMock.Setup(repo => repo.Delete(existingStore));
            _contextMock.Setup(context => context.SaveChanges()).Returns(1);

            // Act
            var result = _storeService.Delete(storeId);

            // Assert
            Assert.True(result.Success);
            Assert.Empty(result.Message);
            _storeRepositoryMock.Verify(repo => repo.Delete(existingStore), Times.Once);
            _contextMock.Verify(context => context.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Delete_ReturnsFailure_WhenStoreNotFound()
        {
            // Arrange
            int storeId = 1;
            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns((StoreModel?)null);

            // Act
            var result = _storeService.Delete(storeId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Store not found.", result.Message);
        }

        [Fact]
        public void Delete_ReturnsFailure_WhenExceptionIsThrown()
        {
            // Arrange
            int storeId = 1;
            var existingStore = new StoreModel { Id = storeId, Name = "Store to Delete" };

            _storeRepositoryMock.Setup(repo => repo.GetById(storeId)).Returns(existingStore);
            _storeRepositoryMock.Setup(repo => repo.Delete(existingStore)).Throws(new Exception());

            // Act
            var result = _storeService.Delete(storeId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("An error ocurred while trying to delete a store.", result.Message);
        }
    }
}
