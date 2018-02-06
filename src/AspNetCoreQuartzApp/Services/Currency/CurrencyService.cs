using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AspNetCoreQuartzApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        protected ConcurrentDictionary<string, Currency> currencies = new ConcurrentDictionary<string, Currency>();

        private ReaderWriterLockSlim lastUpdatedLock = new ReaderWriterLockSlim();

        private DateTime? lastUpdatedUTC;

        public DateTime? LastUpdatedUTC
        {
            get
            {
                lastUpdatedLock.EnterReadLock();
                try
                {
                    return lastUpdatedUTC;
                }
                finally
                {
                    lastUpdatedLock.ExitReadLock();
                }
            }
            protected set
            {
                lastUpdatedLock.EnterWriteLock();
                try
                {
                    lastUpdatedUTC = value;
                }
                finally
                {
                    lastUpdatedLock.ExitWriteLock();
                }
            }
        }

        public CurrencyService()
        {

        }
        
        public IEnumerable<Currency> GetCurrencies()
        {
            return currencies.Select(t => t.Value);
        }

        public void UpdateCurrencies(IEnumerable<Currency> currencies, DateTime lastUpdatedUTC)
        {
            LastUpdatedUTC = lastUpdatedUTC;
            foreach (var currency in currencies)
            {
                this.currencies.AddOrUpdate(currency.CurrencyCode, (currencyCode) => currency, (currencyCode, prevCurrency) => currency);
            }
        }
    }
}
