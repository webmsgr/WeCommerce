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

        /// <summary>
        /// If the user should be FORCED to change their password before they can do anything. Uses middleware.
        /// </summary>
        [Required] 
        public bool ForceChangePassword { get; set; }


        /// <summary>
        /// Is this user an admin?
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; }
    }


    
}
