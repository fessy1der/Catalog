using System;
using System.Collections.Generic;
using Catalog.Models;
using System.Linq;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, DateCreated = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 9, DateCreated = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 9, DateCreated = DateTimeOffset.UtcNow }
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.Id == id).SingleOrDefault();
        }

        public void CreateItem(Item item)
        {
            items.Add(item);
        }

        public void DeleteItem(Guid id)
        {
            var item = items.Where(item => item.Id == id).SingleOrDefault();
            items.Remove(item);
        }

        public void UpdateItem(Item item)
        {
            var index = items.FindIndex(existingitem => existingitem.Id == item.Id);
            items[index] = item;
        }
    }
}