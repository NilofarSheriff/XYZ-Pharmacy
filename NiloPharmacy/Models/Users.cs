using System.ComponentModel.DataAnnotations;

namespace xyzpharmacy.Models
{
    public class Users
    {
        [Key]
        public int u_id { get; set; }
        [Required(ErrorMessage="First Name is Required")]
        [Display(Name ="First Name")]
        [StringLength(100, MinimumLength =3,ErrorMessage="Name should be greater than 3 letters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        [Display(Name = "Last Name")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be greater than 3 letters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter confirm password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        [Display(Name = "Phone Number")]
        [Phone]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public System.DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
    }
}
