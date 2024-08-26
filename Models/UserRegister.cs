using System.ComponentModel.DataAnnotations;
using WeCommerce.Util;

namespace WeCommerce.Models
{
    public class User
    {
        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        [Key]
        public int UserId { get; set; }

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
        /// The user's password hash. NOT PLAINTEXT.
        /// </summary>
        [Required]
        [MaxLength(Geralt.Argon2id.MaxHashSize)]
        [MinLength(Geralt.Argon2id.MinHashSize)]
        public string PasswordHash { get; set; }

    }


    
}
