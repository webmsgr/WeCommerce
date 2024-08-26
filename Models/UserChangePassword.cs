using System.ComponentModel.DataAnnotations;

namespace WeCommerce.Models
{
    public class UserChangePassword
    {

        /// <summary>
        /// The user's old password
        /// </summary>
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

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
