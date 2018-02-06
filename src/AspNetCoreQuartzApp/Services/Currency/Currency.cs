namespace AspNetCoreQuartzApp.Services
{
    public class Currency
    {
        public string CurrencyCode { get; set; }

        public decimal? ToBTC { get; set; }

        public decimal? FromBTC { get; set; }
    }
}