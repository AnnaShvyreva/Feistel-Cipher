﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Key
    {
        public static byte[] Key_Gen(byte[] key_mass, int i) 
        {
            
            /*List<ushort> round_key = new List<ushort> { };
            

            for (int i = 0; i < n; i++)
            {
                round_key.Add(Convert.ToUInt16(Cycle_shift_left(key, i * 12)>>(sizeof(ulong)*8-16)));

                
            }*/
            
            
            /*foreach(ulong aRound_key in round_key)
            {
                Console.WriteLine(aRound_key);
                
            }*/
            //byte[] round_key = new byte[8];
            //Array.Copy(key_mass, round_key, 8);
            long key =  BitConverter.ToInt64(key_mass, 0);
            key = Cycle_shift_left(key, i);
            byte[] round_key = new byte[4];
            Array.Copy(BitConverter.GetBytes(key), 0, round_key, 0, 2);
            //Console.WriteLine(BitConverter.ToInt16(round_key,0));

            return round_key;
        }

        public static long Cycle_shift_left(long k, int i)
        {
            return (k<<i)|(k>>(sizeof(long)*8 - i));
        }
    }
}
