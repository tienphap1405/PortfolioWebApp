using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAndRegistration.Models
{
    //This is the annotation model that represent the attributes that will be implemented on the login page
    public class LoginModel
    {
        //The require functionality will prompt an error when the user input the wrong format or not input.
        [Required(ErrorMessage = "Email is required")]
        public string? UserEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

    }
}
