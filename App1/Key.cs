﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Key
    {
        public static List<byte[]> round_key_mass = new List<byte[]> ();

        public static void Key_Gen(byte[] key_mass, int n) 
        {
            /*List<ushort> round_key = new List<ushort> { };         

            for (int i = 0; i < n; i++)
            {
                round_key.Add(Convert.ToUInt16(Cycle_shift_left(key, i * 12)>>(sizeof(ulong)*8-16)));                
            }            
            
            foreach(ulong aRound_key in round_key)
            {
                Console.WriteLine(aRound_key);                
            }
            byte[] round_key = new byte[8];
            Array.Copy(key_mass, round_key, 8);*/
            
            if (key_mass.Length % 8 != 0)
            {
                byte[] temp = new byte[key_mass.Length + (8 - key_mass.Length % 8)];
                Array.Copy(key_mass, 0, temp, temp.Length - key_mass.Length, key_mass.Length);
                key_mass = temp;
            }

            for (int i = 0; i < n; i++)
            {
                byte[] round_key = new byte[2];
                Array.Copy(BitConverter.GetBytes(Cycle_shift_left(BitConverter.ToUInt64(key_mass, 0), i * 12)), 
                    0, round_key, 0, 2);
                round_key_mass.Add(round_key);
            }

        }

        public static ulong Cycle_shift_left(ulong k, int i)
        {
            if (i > sizeof(ulong) * 8) i = i % (sizeof(ulong) * 8);
            return (k<<i)|(k>>(sizeof(ulong)*8 - i));
        }

        public static byte[] ReturnRoundKey(int i)
        {
            return round_key_mass[i];
        }
    }
}
