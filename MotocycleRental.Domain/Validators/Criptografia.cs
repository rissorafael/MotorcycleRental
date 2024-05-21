using MotorcycleRental.Infra.CrossCutting.ExtensionMethods;
using System.Security.Cryptography;
using System.Text;

namespace MotorcycleRental.Domain.Validators
{
    public static class Criptografia
    {

        public static string Encrypt(string encryptText)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ExatractConfiguration.GetChaveCriptografia);
                aes.IV = Encoding.UTF8.GetBytes(ExatractConfiguration.GetVetorCriptografia);

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(encryptText);
                    }
                    var encrypt = msEncrypt.ToArray();

                    return Convert.ToBase64String(encrypt);
                }
            }


        }

        public static string Decrypt(string decryptText)
        {
            var text = Convert.FromBase64String(decryptText);

            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(ExatractConfiguration.GetChaveCriptografia);
                aes.IV = Encoding.UTF8.GetBytes(ExatractConfiguration.GetVetorCriptografia);

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var msDecrypt = new MemoryStream(text))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }

        }
    }
}
