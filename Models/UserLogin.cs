using System.ComponentModel.DataAnnotations;
using WeCommerce.Util;

namespace WeCommerce.Models
{
    public class UserLogin
    {
        /// <summary>
        /// The user's username
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }



        /// <summary>
        /// The user's password. Not hashed.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Checks if the UserLogin matches a User (for login purposes)
        /// </summary>
        /// <param name="u">The user to check</param>
        /// <returns>If the user should be logged in</returns>

        public bool Matches(User u)
        {
            return u.Username == Username 
                   && Crypto.VerifyPassword(u.PasswordHash, Password);
        }
    }
}
