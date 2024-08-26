using System.Text;

namespace WeCommerce.Util
{
    /// <summary>
    /// Crypto utils taken from dotnetris
    /// </summary>
    public static class Crypto
    {

        public const int Iterations = 3;
        public const int MemorySize = 67108864;

        /// <summary>
        /// Securely hash a password with Argon2id
        /// </summary>
        /// <param name="password">Password to hash</param>
        /// <returns>The hashed password</returns>
        public static string HashPassword(string password)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(password);
            Span<byte> output = stackalloc byte[Geralt.Argon2id.MaxHashSize];
            Geralt.Argon2id.ComputeHash(output, bytePassword.AsSpan(), Iterations, MemorySize); //adjust as needed
            return Encoding.UTF8.GetString(output);
        }
        /// <summary>
        /// Verify a password matches a hash
        /// </summary>
        /// <param name="hash">Hashed password</param>
        /// <param name="password">Password to check</param>
        /// <returns>True if the password matches, false otherwise.</returns>
        public static bool VerifyPassword(string hash, string password)
        {
            byte[] bytePassword = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = Encoding.UTF8.GetBytes(hash);
            return Geralt.Argon2id.VerifyHash(hashBytes.AsSpan(), bytePassword.AsSpan());
        }

        /// <summary>
        /// Check if a hash needs to be rehashed. If this method returns true, the password should be rehashed and stored in the database
        /// </summary>
        /// <param name="hash">The hash to check</param>
        /// <returns>If the password should be rehashed</returns>
        public static bool ShouldRehash(string hash)
        {
            return Geralt.Argon2id.NeedsRehash(Encoding.UTF8.GetBytes(hash).AsSpan(), Iterations, MemorySize);
        }
    }
}
