using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Key
    {
        public static List<ushort> Key_Gen(ulong key, int n) 
        {
            
            List<ushort> round_key = new List<ushort> { };
            

            for (int i = 0; i < n; i++)
            {
                round_key.Add(Convert.ToUInt16(Cycle_shift_left(key, i * 12)>>(sizeof(ulong)*8-16)));

                //round_key.Add(Convert.ToUInt16((key << 2) >> (sizeof(ulong) * 8 - 16)));
                
            }
            //Console.WriteLine((5432 << 0)>>48);


            
            foreach(ulong aRound_key in round_key)
            {
                Console.WriteLine(aRound_key);
                
            }
            /*
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(round_key[i]);
            }
            */

            return round_key;
        }

        public static ulong Cycle_shift_left(ulong k, int i)
        {
            return (k<<i)|(k>>(sizeof(ulong)*8 - i));
        }
    }
}
