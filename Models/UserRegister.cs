using System.ComponentModel.DataAnnotations;
using WeCommerce.Util;

namespace WeCommerce.Models
{
    public class UserRegister
    {

        /// <summary>
        /// The user's email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }



        /// <summary>
        /// Confirm email address
        /// </summary>
        [Required]
        [EmailAddress]
        [Compare("Email")]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        /// <summary>
        /// The user's username
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }



        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        /// <summary>
        /// The confirm password
        /// </summary>
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public User ToUser()
        {
            return new User
            {
                Email = Email,
                Username = Username,
                PasswordHash = Crypto.HashPassword(Password)
            };
        }

    }


    
}
