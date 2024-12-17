using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankAPI.Entities
{
    public class Account
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Account Number is required.")]
        public string AccountNumber { get; set; }


        [Range(0, double.MaxValue, ErrorMessage = "Balance must be greater than or equal to 0.")]
        public decimal Balance { get; set; }


        [Required(ErrorMessage = "Currency is required.")]
        [StringLength(3, ErrorMessage = "Currency must be a 3 letter")]
        public string Currency { get; set; }

        [Required(ErrorMessage = "Account Type is required.")]
        public string AccountType { get; set; }


        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }



        [ForeignKey("User")]
        public int UserID { get; set; }

        [JsonIgnore]
        public  User? User { get; set; }
    }
}