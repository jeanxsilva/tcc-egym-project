using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace eGYM.Core
{
    public class SecurityHash
    {
        private HashAlgorithm hashAlgoritm;

        public SecurityHash(HashAlgorithm hashAlgoritm)
        {
            this.hashAlgoritm = hashAlgoritm;
        }

        public string CryptoPassword(string password)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(password);
            byte[] encryptedPassword = this.hashAlgoritm.ComputeHash(encoded);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte caracter in encryptedPassword)
            {
                stringBuilder.Append(caracter.ToString("X2"));
            }
            return stringBuilder.ToString();
        }

        public bool CompairPassword(string password, string passwordToCompair)
        {
            if (string.IsNullOrEmpty(passwordToCompair))
            {
                throw new NullReferenceException("The password must be setted");
            }

            string comparable = this.CryptoPassword(passwordToCompair);
            return password.Equals(comparable);
        }
    }
}