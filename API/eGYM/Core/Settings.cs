using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eGYM
{
    public static class Settings
    {
        public static string Secret = "fedaf7d8863b48e197b9287d492b708e";
        public static byte[] SecretByte
        {
            get
            {
                return Encoding.ASCII.GetBytes(Settings.Secret);
            }
        }
    }
}
