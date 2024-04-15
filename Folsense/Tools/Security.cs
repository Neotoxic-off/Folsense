using Folsense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Folsense.Tools
{
    public class Security
    {
        private static byte[] GetKey()
        {
            return (ISettings.Key);
        }

        public static byte[] Encrypt(byte[] input)
        {
            byte[] output = new byte[input.Length];
            byte[] key = GetKey();

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = (byte)(input[i] ^ key[i % key.Length]);
            }

            return output;
        }

        public static byte[] Decrypt(byte[] input)
        {
            return Encrypt(input);
        }
    }
}
