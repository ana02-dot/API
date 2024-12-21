namespace BankAPI.Interfaces
{
    public interface ICurrencyConverter
    {
        decimal Convert(decimal amount, string fromCurrency, string toCurrency);

    }
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly Dictionary<string, decimal> _exchangeRates;

        public CurrencyConverter()
        {
            
            _exchangeRates = new Dictionary<string, decimal>
            {
                { "USD-EUR", 0.85m },
                { "EUR-USD", 1.18m },
                { "USD-GBP", 0.75m },
                { "GBP-USD", 1.33m }
            };

        }

        public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
        {
            if (fromCurrency == toCurrency) return amount;

            var key = $"{fromCurrency}-{toCurrency}";
            if (!_exchangeRates.TryGetValue(key, out var rate))
            {
                throw new KeyNotFoundException($"Exchange rate not found for {key}.");
            }

            return amount * rate;
        }
    }
}