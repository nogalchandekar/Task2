using System.ComponentModel.DataAnnotations;

namespace Task2.Models
{
    public class RegristerModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Email ID is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailID { get; set; }

        [Required(ErrorMessage = "Password is required.")]

        public string Password { get; set; }



        [Required(ErrorMessage = "Date of Birth is required.")]

        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]

        public string Gender { get; set; }

        [Required(ErrorMessage = "Contact Number is required.")]

        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Dept is required.")]

        public string Dept { get; set; }

        [Required(ErrorMessage = "Role is required.")]

        public string Role { get; set; }

        [Required(ErrorMessage = "Fee is required.")]

        public decimal Fee { get; set; }

        [Required(ErrorMessage = "status is required.")]

        public string Status { get; set; }

        public string Qualification { get; set; }

    }
}
