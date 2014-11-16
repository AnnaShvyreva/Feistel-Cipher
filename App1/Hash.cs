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
        
        /*public static void HashGen(byte[] key_mass)
        {
            if (key_mass.Length % 8 != 0)
            {
                byte[] temp = new byte[key_mass.Length + (8 - key_mass.Length % 8)];
                Array.Copy(key_mass, 0, temp, temp.Length - key_mass.Length, key_mass.Length);
                key_mass = temp;
            }
            //round_key_mass.Add(new byte[8] {11, 11, 11, 11, 11, 11, 11, 11});
            round_key_mass.Add(key_mass);
        }*/

        public static byte[] ReturnRoundKey(int i)
        {
            return round_key_mass[i];
        }

        public static void AddRoundKey(byte[] key, int i)
        {
            Key_Gen(key,i);
            //round_key_mass.Add(key);
        }

        public static void Key_Gen(byte[] key_mass, int n)
        {
            if (key_mass.Length % 8 != 0)
            {
                byte[] temp = new byte[key_mass.Length + (8 - key_mass.Length % 8)];
                Array.Copy(key_mass, 0, temp, temp.Length - key_mass.Length, key_mass.Length);
                key_mass = temp;
            }

            /*for (int i = 0; i < n; i++)
            {
                byte[] round_key = new byte[2];
                Array.Copy(BitConverter.GetBytes(Cycle_shift_left(BitConverter.ToUInt64(key_mass, 0), i * 12)),
                    0, round_key, 0, 2);
                round_key_mass.Add(round_key);
            }*/

            byte[] round_key = new byte[2];
            Array.Copy(key_mass, 0, round_key, 0, 2);
            round_key_mass.Add(round_key);


        }

    }
}
