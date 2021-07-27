using System;
using Xunit;
using Catalog.Api.Repositories;
using Catalog.Api.Models;
using Catalog.Api.Controllers;
using Catalog.Api.Dtos;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FluentAssertions;

namespace Catalog.Tests
{
    public class ItemsControllerTests
    {
        private readonly Mock<IItemsRepository> repositoryStub = new();
        private readonly Mock<ILogger<ItemsController>> loggerStub = new();
        private readonly Random rand = new();

        [Fact]
        public async Task GetItemAsync_WithUnexistingItem_ReturnNotFound()
        {
            //Arrange
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetItemAsync_WithExistingItem_ReturnExpectedItem()
        {
            //Arrange
            var expectedItem = CreateRandomItem();

            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync(expectedItem);

            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetItemAsync(Guid.NewGuid());

            //Assert
            result.Value.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<Item>());
        }

        [Fact]
        public async Task GetItemsAsync_WithExistingItem_ReturnsAllItems()
        {
            // Arrange
            var expectedItems = new[] { CreateRandomItem(), CreateRandomItem(), CreateRandomItem() };
            repositoryStub.Setup(repo => repo.GetItemsAsync()).ReturnsAsync(expectedItems);
            var controller = new ItemsController(repositoryStub.Object, loggerStub.Object);

            // Act
            var actualItems = await controller.GetItemsAsync();

            //Assert
            actualItems.Should().BeEquivalentTo(expectedItems, options => options.ComparingByMembers<Item>());
        }

        private Item CreateRandomItem()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                Price = rand.Next(1000),
                DateCreated = DateTimeOffset.UtcNow
            };
        }

    }
}
