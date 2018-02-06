using AspNetCoreQuartzApp.Services;
using Newtonsoft.Json.Linq;
using Quartz;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCoreQuartzApp.Jobs
{
    [DisallowConcurrentExecution]
    public class CurrencyJob : BaseJob<CurrencyJob>
    {
        public CurrencyJob() : base()
        {
        }

        protected async Task<IEnumerable<Currency>> GetCurrencyDataAsync()
        {
            var currencyCodes = await GetCurrencyCodesAsync();
            if (currencyCodes == null)
            {
                return null;
            }

            List<Currency> currencies = new List<Currency>();
            foreach (var currencyCode in currencyCodes)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        client.BaseAddress = new Uri("https://blockchain.info");
                        var response = await client.GetAsync($"/tobtc?currency={currencyCode}&value=1");
                        response.EnsureSuccessStatusCode();

                        var stringResult = await response.Content.ReadAsStringAsync();
                        decimal toBTC = decimal.Parse(stringResult);

                        currencies.Add(new Currency()
                        {
                            CurrencyCode = currencyCode,
                            ToBTC = toBTC,
                            FromBTC = 1m / toBTC
                        });
                    }
                    catch (HttpRequestException)
                    {
                        return null;
                    }
                }
            }
            return currencies;
        }

        protected async Task<IEnumerable<string>> GetCurrencyCodesAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://blockchain.info");
                    var response = await client.GetAsync("/ticker");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    var jobj = JObject.Parse(stringResult);
                    IEnumerable<string> result = jobj
                        .Properties()
                        .ToList()
                        .Select(t => t.Name)
                        .ToList();

                    return result;
                }
                catch (HttpRequestException)
                {
                    return null;
                }
            }
        }

        protected override async Task ExecuteAsync(IServiceProvider serviceProvider)
        {
            await CreateJobInformationRecordAsync();
            try
            {
                ICurrencyService currencyService = serviceProvider.GetService<ICurrencyService>();

                var currencies = await GetCurrencyDataAsync();

                if (currencies == null)
                {
                    await UpdateJobInformationRecordAsync("Job finished with warning.", "Unable to retrieve data from 3rd party.");
                    return;
                }

                currencyService.UpdateCurrencies(currencies, DateTime.UtcNow);
                await UpdateJobInformationRecordAsync("Job has finished correctly.");
                return;
            }
            catch (Exception exception)
            {
                await UpdateJobInformationRecordAsync("Job has finished with error.", exception);
                return;
            }
        }

    }
}
