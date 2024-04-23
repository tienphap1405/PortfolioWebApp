using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAndRegistration.Models
{
        //This is the registration model that represent the attributes that will be implemented on the login page
	public class RegistrationModel : IValidatableObject
	{
        //These "Required" annotation required the users to enter accurately, if not then it will prompt errors
        [Required(ErrorMessage = "Email is required")]
        public string? userEmail { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        public string? confirmPassword { get; set; }
        [Required(ErrorMessage = "Address 1 is required")]
        public string? address1 { get; set; }
        [Required(ErrorMessage = "Address 2 is required")]
        public string? address2 { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? city { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }
        [Required(ErrorMessage = "Postal code is required")]
        public string?	 postalCode { get; set; }

        //This function uses to validate the password, is the password that the user input is not match then the code will prompt errors
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
            if (Password != confirmPassword)
            {
                yield return new ValidationResult("Passwords do not match.", new[] { nameof(confirmPassword) });
            }
        }
	}
}
