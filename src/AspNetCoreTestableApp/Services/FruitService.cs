using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AspNetCoreTestableApp.Services
{
    public class FruitService : IFruitService
    {
        protected Dictionary<int, Fruit> FruitsCollection { get; set; }

        public FruitService()
        {
            this.FruitsCollection = new Dictionary<int, Fruit>();
        }

        public async Task AddFruitAsync(Fruit fruit)
        {
            if (fruit == null)
            {
                await Task.FromException(new ArgumentException("Fruit can't be null."));
            }

            if (FruitsCollection.ContainsKey(fruit.FruitId))
            {
                await Task.FromException(new ArgumentException("Fruit with same Id already exist."));
            }
            else
            {
                FruitsCollection.Add(fruit.FruitId, fruit);
                await Task.CompletedTask;
            }
        }

        public async Task DeleteFruitAsync(int fruitId)
        {
            FruitsCollection.Remove(fruitId);
            await Task.CompletedTask;
        }

        public async Task<Fruit> GetFruitAsync(int fruitId)
        {
            if (FruitsCollection.ContainsKey(fruitId))
            {
                return await Task.FromResult(FruitsCollection[fruitId]);
            }
            else
            {
                return await Task.FromResult<Fruit>(null);
            }
        }

        public async Task<IEnumerable<Fruit>> GetFruitsAsync()
        {
            return await Task.FromResult(FruitsCollection.Values.ToList());
        }
    }
}
