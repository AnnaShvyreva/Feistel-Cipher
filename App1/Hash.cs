using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Hash //формирование ключей хеш-функцией
    {
        public static List<byte[]> round_key_mass = new List<byte[]>();

        public void HashGen()
        {
            round_key_mass.Add(new byte[8] {11, 11, 11, 11, 11, 11, 11, 11});
        }

        public static byte[] ReturnRoundKey(int i)
        {
            return round_key_mass[i];
        }

        public static void AddRoundKey(byte[] key)
        {
            round_key_mass.Add(key);
        }
    }
}
