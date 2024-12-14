using System.ComponentModel.DataAnnotations;

namespace BankAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName{ get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName{ get; set; }

        [Required(ErrorMessage = "Personal Number is required.")]
        public string PersonalNumber {  get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }

    }
}
