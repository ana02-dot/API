using System.ComponentModel.DataAnnotations;

namespace BankAPI.Entities
{
    public class BankTransactions
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "From Account ID is required.")]
        public int FromAccountID { get; set; }

        [Required(ErrorMessage = "To Account ID is required.")]
        public int ToAccountID { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }
    }
}
