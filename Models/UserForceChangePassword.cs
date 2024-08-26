using System.ComponentModel.DataAnnotations;

namespace WeCommerce.Models
{
    public class UserForceChangePassword
    {


        /// <summary>
        /// The user's new password.
        /// </summary>
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// The confirm password
        /// </summary>
        [Required]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
