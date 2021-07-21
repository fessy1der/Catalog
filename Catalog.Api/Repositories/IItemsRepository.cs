using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Api.Models;

namespace Catalog.Api.Repositories
{
    public interface IItemsRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task DeleteItemAsync(Guid id);
        Task UpdateItemAsync(Item item);
        Task CreateItemAsync(Item item);
    }
}