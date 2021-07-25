using System;
using Xunit;
using Catalog.Api.Repositories;
using Catalog.Api.Models;
using Moq;

namespace Catalog.Tests
{
    public class ItemsControllerTests
    {
        [Fact]
        public void GetItemAsync_WithUnexistingItem_ReturnNotFound()
        {
            var repositoryStub = new Mock<IItemsRepository>();
            repositoryStub.Setup(repo => repo.GetItemAsync(It.IsAny<Guid>())).ReturnsAsync((Item)null);
        }
    }
}
