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
        public static List<byte[]> hash_mass = new List<byte[]>();
        
        public static byte[] ReturnRoundKey(int i)
        {
            return round_key_mass[i];
        }

        public static void AddRoundKey(byte[] key, int i)
        {
            Key_Gen(key,i);
        }

        public static void Key_Gen(byte[] key_mass, int n)
        {
            if (key_mass.Length % 2 != 0)
            {
                byte[] temp = new byte[key_mass.Length + (2 - key_mass.Length % 2)];
                Array.Copy(key_mass, 0, temp, temp.Length - key_mass.Length, key_mass.Length);
                key_mass = temp;
            }

            byte[] round_key = new byte[2];
            Array.Copy(key_mass, 0, round_key, 0, 2);
            round_key_mass.Add(round_key);

        }

        public static void H()
        {
            int[] mass = new int[10] {0,0,0,0,0,0,0,0,0,0};
            int j = -1;
            foreach (byte[] a in hash_mass)
            {
                j++;
                mass[BitConverter.ToUInt16(ReturnHash(j),0) % 10]++;
            }

            for (int i = 0; i < 10; i++)
                Console.WriteLine(mass[i]);

        }

        public static void AddHash(byte[] key)
        {
            hash_mass.Add(key);
        }

        public static byte[] ReturnHash(int i)
        {
            return hash_mass[i];
        }

    }
}
