using System.ComponentModel.DataAnnotations;
using WeCommerce.Util;

namespace WeCommerce.Models
{
    public class User
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }



        /// <summary>
        /// The user's username
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Username { get; set; }



        /// <summary>
        /// The user's password hash.
        /// </summary>
        [Required]
        [MaxLength(Geralt.Argon2id.MaxHashSize)]
        public string PasswordHash { get; set; }


        

    }


    
}
