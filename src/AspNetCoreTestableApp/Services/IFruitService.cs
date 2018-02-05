using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreTestableApp.Services
{
    public interface IFruitService
    {
        Task<IEnumerable<Fruit>> GetFruitsAsync();
        Task<Fruit> GetFruitAsync(int fruitId);
        Task AddFruitAsync(Fruit fruit);
        Task DeleteFruitAsync(int fruitId);
    }
}
