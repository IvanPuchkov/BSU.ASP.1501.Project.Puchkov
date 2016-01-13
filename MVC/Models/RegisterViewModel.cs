using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Field Display Name is required")]
        [Display(Name = "Display Name")]
        [StringLength(16, ErrorMessage = "Display name should have 4- 16 characters", MinimumLength = 4)]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [StringLength(16, ErrorMessage = "Password should have 6- 16 characers", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field Email is required")]
        [EmailAddress]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }
        
    }
}