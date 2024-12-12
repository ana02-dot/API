namespace BankAPI.Entities
{
    public class Transaction
    {
        public int ID { get; set; }
        public int FromAccountID { get; set; }
        public int ToAccountID { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }

    }
}
