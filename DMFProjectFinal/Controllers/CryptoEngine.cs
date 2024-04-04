using System;
using System.Security.Cryptography;
using System.Text;

namespace DMFProjectFinal.Controllers
{
    public class CryptoEngine
    {
        /// <summary>
        /// Method for encrypted the value (use for Mine Mitra)
        /// </summary>
        /// <param name="input">holds value in string formate</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            if (input != null && input != "")
            {
                string key = System.Configuration.ConfigurationManager.AppSettings["EncriptKey"];
                byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                string encValue = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                return encValue;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Method for decrypted the value  (use for Mine Mitra)
        /// </summary>
        /// <param name="input">holds value in string formate</param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            if (input != null && input != "")
            {
                input = input.Replace(" ", "+");
                string key = System.Configuration.ConfigurationManager.AppSettings["EncriptKey"];
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            else
            {
                return "";
            }
        }

    }
}