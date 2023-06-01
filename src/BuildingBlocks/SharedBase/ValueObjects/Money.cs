namespace SharedBase.ValueObjects
{
    public class Money
    {
        public static Money ForView(string currency, decimal amount) => new Money(currency, amount);
        //[Nullable]
        private Money() { }
        private Money(string currency, decimal amount, DateTime time)
        {
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentNullException(nameof(currency));
            if (amount < 0M) throw new ArgumentOutOfRangeException(nameof(amount));
            Currency = currency.Trim(); Amount = amount; Time = time;
        }
        public static Money Create(string currency, decimal amount, DateTime? time = null) =>
            new Money(currency, amount, time != null ? time.Value : DateTime.UtcNow);
        private Money(string currency, decimal amount)
        {
            Currency = currency.Trim(); Amount = amount; Time = DateTime.UtcNow;
        }
        /// <summary>
        /// Store Currency Id
        /// </summary>
        public string Currency { get; private set; } // The setters are for elastic De-serialize only
        public decimal Amount { get; private set; }
        public DateTime Time { get; set; }
        public override string ToString() => $"{Currency} {Amount:N2}";
        public override bool Equals(object obj)
        {
            return obj is Money money &&
                   Currency == money.Currency &&
                   Amount == money.Amount &&
                   Time == money.Time;
        }
        public override int GetHashCode()
        {
            var hashCode = 624119691;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Currency);
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            return hashCode;
        }
        public static bool operator ==(Money left, Money right)
        {
            return EqualityComparer<Money>.Default.Equals(left, right);
        }
        public static bool operator !=(Money left, Money right)
        {
            return !(left == right);
        }
    }
}
