using AspNetCoreQuartzApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AspNetCoreQuartzApp.Controllers
{
    [Route("api")]
    public class CurrencyController : Controller
    {
        protected ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
        }
        
        [HttpGet("currency")]
        public IActionResult GetCurrencies()
        {
            try
            {
                var currencies = currencyService.GetCurrencies();

                return Ok(currencies);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
