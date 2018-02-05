using AspNetCoreTestableApp.Controllers;
using AspNetCoreTestableApp.Models;
using AspNetCoreTestableApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTestableApp.Tests
{
    public class FruitControllerTests
    {
        [Fact]
        public async Task GetFruitById_ReturnsNotFound_WhenFruitIsNotFound()
        {
            int fruitId = 1;

            var mockRepo = new Mock<IFruitService>();
            mockRepo
                .Setup(t => t.GetFruitAsync(fruitId))
                .Returns(Task.FromResult<Fruit>(null));

            var controller = new FruitController(mockRepo.Object);

            // Act
            var result = await controller.GetAsync(fruitId: fruitId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetFruitById_ReturnsOkResult_WhenFruitIsFound()
        {
            int fruitId = 1;

            var fruit = new Fruit
            {
                FruitId = fruitId,
                Name = "Apple"
            };

            var mockRepo = new Mock<IFruitService>();
            mockRepo
                .Setup(t => t.GetFruitAsync(fruitId))
                .Returns(Task.FromResult(fruit));

            var controller = new FruitController(mockRepo.Object);

            // Act
            var result = await controller.GetAsync(fruitId: fruitId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetFruitById_ReturnsCorrectFruit_WhenFruitIsFound()
        {
            int fruitId = 1;

            var fruit = new Fruit
            {
                FruitId = fruitId,
                Name = "Apple"
            };

            var mockRepo = new Mock<IFruitService>();
            mockRepo
                .Setup(t => t.GetFruitAsync(fruitId))
                .Returns(Task.FromResult(fruit));

            var controller = new FruitController(mockRepo.Object);

            // Act
            var result = await controller.GetAsync(fruitId: fruitId);

            // Assert
            var resultFruit = (result as OkObjectResult).Value as FruitViewModel;

            Assert.NotNull(resultFruit);

            Assert.Equal(resultFruit.Name, fruit.Name);
            Assert.Equal(resultFruit.FruitId, fruit.FruitId);
        }

        [Fact]
        public async Task DeleteFruitById_ReturnsOkResult_WhenFruitIsDeleted()
        {
            int fruitId = 1;

            var mockRepo = new Mock<IFruitService>();
            mockRepo
                .Setup(t => t.DeleteFruitAsync(fruitId))
                .Returns(Task.CompletedTask);

            var controller = new FruitController(mockRepo.Object);

            // Act
            var result = await controller.DeleteAsync(fruitId: fruitId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateFruit_ReturnsOkResult_WhenFruitIsCreated()
        {
            var mockRepo = new Mock<IFruitService>();
            mockRepo
                .Setup(t => t.AddFruitAsync(It.IsAny<Fruit>()))
                .Returns(Task.CompletedTask);

            var controller = new FruitController(mockRepo.Object);

            // Act
            var result = await controller.CreateAsync(
                new FruitViewModel()
                {
                    FruitId = 1,
                    Name = "Apple"
                });

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateFruit_ReturnsBadRequest_WhenFruitIsNotCreated()
        {
            var mockRepo = new Mock<IFruitService>();
            mockRepo
                .Setup(t => t.AddFruitAsync(It.IsAny<Fruit>()))
                .Throws(new ArgumentException());

            var controller = new FruitController(mockRepo.Object);

            // Act
            var result = await controller.CreateAsync(
                new FruitViewModel()
                {
                    FruitId = 1,
                    Name = "Apple"
                });

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
