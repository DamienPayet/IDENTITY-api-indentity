using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Di2P1G3.Authentication.Core
{
    /// <summary>
    /// Password encryption related methods provider.
    /// </summary>
    public static class PasswordEncryption
    {
        private const int SaltLengthLimit = 32;

        /// <summary>
        /// Generate salted hash from string value and salt.
        /// </summary>
        /// <param name="plainText">Plain text to hash and salt.</param>
        /// <param name="salt">Salt used.</param>
        /// <returns>Salted hash from plain text.</returns>
        public static byte[] GenerateSaltedHash(string plainText, byte[] salt)
        {
            return GenerateSaltedHash(Encoding.UTF8.GetBytes(plainText), salt);            
        }
        
        /// <summary>
        /// Generate salted hash from byte arrays.
        /// </summary>
        /// <param name="plainText">Plain text to hash and salt.</param>
        /// <param name="salt">Salt used.</param>
        /// <returns>Salted hash from plain text.</returns>
        private static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            return algorithm.ComputeHash(plainText.Concat(salt).ToArray());            
        }

        /// <summary>
        /// Check if <paramref name="plainText"/> corresponds to <paramref name="hashedPassword"/> with corresponding <paramref name="salt"/>.
        /// </summary>
        /// <param name="plainText">Plain password.</param>
        /// <param name="hashedPassword">Hashed password.</param>
        /// <param name="salt">Salt used for <paramref name="hashedPassword"/>.</param>
        /// <returns>True if password corresponds, false otherwise.</returns>
        public static bool CheckPasswords(string plainText, byte[] hashedPassword, byte[] salt)
        {
            var newHashPassword = GenerateSaltedHash(plainText, salt);
            
            return newHashPassword.SequenceEqual(hashedPassword);
        }
        
        /// <summary>
        /// Generate a random salt from a <see cref="RNGCryptoServiceProvider"/>.
        /// </summary>
        /// <returns>Array of byte salt.</returns>
        public static byte[] GetSalt()
        {
            var salt = new byte[SaltLengthLimit];
            using var random = new RNGCryptoServiceProvider();
            random.GetNonZeroBytes(salt);

            return salt;
        }
    }
}