using System.Security.Cryptography;
using System.Text;

namespace ms_evva_core.Security
{
    public class Encryption
    {
        private static readonly string Key = "EvvaCoreSecretKey123!@#"; // 24 bytes para TripleDES
        private static readonly string IV = "EvvaCoreIV16"; // 16 bytes para IV

        public static string Encrypt(string plainText)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(IV);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                using (var des = TripleDES.Create())
                {
                    des.Key = keyBytes;
                    des.IV = ivBytes;
                    des.Mode = CipherMode.CBC;
                    des.Padding = PaddingMode.PKCS7;

                    using (var encryptor = des.CreateEncryptor())
                    {
                        byte[] encryptedBytes = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criptografar mensagem", ex);
            }
        }

        public static string Decrypt(string encryptedText)
        {
            try
            {
                byte[] keyBytes = Encoding.UTF8.GetBytes(Key);
                byte[] ivBytes = Encoding.UTF8.GetBytes(IV);
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                using (var des = TripleDES.Create())
                {
                    des.Key = keyBytes;
                    des.IV = ivBytes;
                    des.Mode = CipherMode.CBC;
                    des.Padding = PaddingMode.PKCS7;

                    using (var decryptor = des.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao descriptografar mensagem", ex);
            }
        }

        public static string GenerateHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool ValidateHash(string input, string hash)
        {
            string computedHash = GenerateHash(input);
            return computedHash.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
} 