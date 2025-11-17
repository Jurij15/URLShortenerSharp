using System;
using System.Security.Cryptography;
using System.Text;

namespace URLShortenerSharp.Helpers
{
    public static class StringHasher
    {
        /// <summary>
        /// Hashes an input string into a short, deterministic 6-character code.
        /// Uses SHA256 for cryptographic safety and Base32 encoding for compact output.
        /// </summary>
        public static string ShortHash(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Use a fixed buffer size and a using block to ensure disposal (memory safe)
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hash = sha.ComputeHash(bytes);

                // Encode in a limited character set (Base32 for alphanumeric safety)
                string base32 = Base32Encode(hash);

                // Limit to max 6 chars
                return base32[..6];
            }
        }

        /// <summary>
        /// Simple Base32 encoder (RFC 4648)
        /// </summary>
        private static string Base32Encode(byte[] data)
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            StringBuilder result = new StringBuilder((data.Length + 4) / 5 * 8);

            int buffer = data[0];
            int next = 1;
            int bitsLeft = 8;
            while (bitsLeft > 0 || next < data.Length)
            {
                if (bitsLeft < 5)
                {
                    if (next < data.Length)
                    {
                        buffer <<= 8;
                        buffer |= data[next++] & 0xFF;
                        bitsLeft += 8;
                    }
                    else
                    {
                        int pad = 5 - bitsLeft;
                        buffer <<= pad;
                        bitsLeft += pad;
                    }
                }
                int index = buffer >> bitsLeft - 5 & 0x1F;
                bitsLeft -= 5;
                result.Append(alphabet[index]);
            }

            return result.ToString();
        }
    }

}
