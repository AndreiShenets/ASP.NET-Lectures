using System;
using System.Collections.Generic;

namespace AspNetCoreQuartzApp.Services
{
    public interface ICurrencyService
    {
        DateTime? LastUpdatedUTC { get; }

        IEnumerable<Currency> GetCurrencies();

        void UpdateCurrencies(IEnumerable<Currency> currencies, DateTime dateTime);
    }
}
