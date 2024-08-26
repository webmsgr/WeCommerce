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
        public string Email { get; set; }



        /// <summary>
        /// The user's username
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }



        /// <summary>
        /// The user's password
        /// </summary>
        [Required]
        public string Password { get; set; }


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
