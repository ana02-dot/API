namespace BankAPI.Entities
{
    public class Account
    {
        public int ID { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public string AccountType { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

    }
}
