using System;
using System.Collections.Generic;
using System.Numerics;
namespace Advent2020.ComboBreaker
{
    public class Rsa
    {
        BigInteger Modulus;
        public Rsa(int modulus)
        {
            Modulus = modulus;
        }
        public int Encrypt(int message, int exponent)
        {
            return (int)BigInteger.ModPow(message, exponent, Modulus);
        }
        public int FindExponent(int message, int encrypted)
        {
            BigInteger res;
            for (int i = 0; i < 100000000; i++)
            {
                res = BigInteger.ModPow(message, i, Modulus);
                if (res == encrypted)
                {
                    return i;
                }
            }
            throw new Exception($"exponent not found");
        }
        public int FindEncryptionKey(int pk1, int pk2)
        {
            // int exp1 = FindExponent(7, pk1);
            int exp2 = FindExponent(7, pk2);
            int encryptionKey = Encrypt(pk1, exp2);
            // if (encryptionKey != Encrypt(pk1, exp2))
            // {
            //     throw new Exception("Encryption keys not equal");
            // }
            return encryptionKey;
        }
        public int FindEncryptionKeyFromFile(string filename)
        {
            List<int> keys = FileReader.ReadFileOfInts(filename);
            return FindEncryptionKey(keys[0], keys[1]);
        }
    }
}