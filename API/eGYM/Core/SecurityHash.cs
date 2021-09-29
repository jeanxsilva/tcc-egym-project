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
                stringBuilder.Append(caracter.ToString());
            }

            return stringBuilder.ToString();
        }

        public bool CompairPassword(string password, string passwordToCompair)
        {
            if (string.IsNullOrEmpty(passwordToCompair))
            {
                throw new NullReferenceException("The password must be setted");
            }

            return password.Equals(this.CryptoPassword(passwordToCompair));
        }
    }
}