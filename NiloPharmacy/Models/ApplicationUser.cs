using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace xyzpharmacy.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

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
        public Gender Gender { get; set; }
    }
     public enum Gender
    {
        Female=1,
        Male=2,
        Others=3
    }
}
