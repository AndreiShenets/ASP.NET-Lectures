using AspNetCoreTestableApp.Models;
using AspNetCoreTestableApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTestableApp.Controllers
{
    [Route("api")]
    public class FruitController : Controller
    {
        protected IFruitService FruitService { get; set; }
        
        public FruitController(IFruitService fruitService)
        {
            this.FruitService = fruitService ?? throw new ArgumentNullException(nameof(fruitService));
        }

        [HttpGet("fruit")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var allFruits = await FruitService.GetFruitsAsync();
                var resultFruits = allFruits.Select(t =>
                    new FruitViewModel()
                    {
                        FruitId = t.FruitId,
                        Name = t.Name
                    });

                return Ok(resultFruits);
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

        [HttpGet("fruit/{fruitId:int}")]
        public async Task<IActionResult> GetAsync(int fruitId)
        {
            try
            {
                var fruit = await FruitService.GetFruitAsync(fruitId);
                if (fruit == null)
                {
                    return NotFound();
                }

                var result = new FruitViewModel()
                {
                    FruitId = fruit.FruitId,
                    Name = fruit.Name
                };

                return Ok(result);
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

        [HttpPost("fruit")]
        public async Task<IActionResult> CreateAsync([FromBody] FruitViewModel fruitViewModel)
        {
            try
            {
                var fruit = new Fruit()
                {
                    FruitId = fruitViewModel.FruitId,
                    Name = fruitViewModel.Name
                };

                await FruitService.AddFruitAsync(fruit);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }

        [HttpDelete("fruit/{fruitId:int}")]
        public async Task<IActionResult> DeleteAsync(int fruitId)
        {
            try
            {
                await FruitService.DeleteFruitAsync(fruitId);
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(500);
            }
        }
    }
}
