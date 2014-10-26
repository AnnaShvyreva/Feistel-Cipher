using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Feistel_Cipher
    {
        public static void Feistel(ulong text, ulong key, int n)
        {
            List<ushort> round_key = Key.Key_Gen(key, n);
            List<ulong> block64 = new List<ulong> { };
            List<ulong> block64e = new List<ulong> { };

            //разделить текст на блоки по 64 бита

            foreach (ushort aBlock64 in block64)
            {
                ulong buf = aBlock64;
                for (int i = 0; i < n - 1; i++)
                {
                    buf = Round(buf, round_key[i], 0);
                }
                block64e.Add(Round(buf, round_key[n-1], 1));

            }

        }

        public static ulong Round(ulong block, ushort key, int n)
        {
            List<ushort> block16 = new List<ushort> { };
            for (int i = 0; i < sizeof(ulong); i=i+sizeof(ushort))
            {
                block16.Add(Convert.ToUInt16((block << (i * sizeof(ushort))) >> 3 * sizeof(ushort)));

            }

            List<ushort> block16e = new List<ushort> { };
            block16e.Add(block16[1]);//4
            block16e.Add(block16[2]);//3
            block16e.Add((ushort)(function(block16[1], block16[2]) ^ block16[3]));//2
            block16e.Add((ushort)(block16e[3] ^ (block16[4] ^ key)));//1
            //соединить используя маску и сдвиги, метод возвращает этот ulong
            if (n == 0)
            {
                return (((block16e[4] & 0xFFFFFFFFFFFFFFFF) << 48) | ((block16e[3] & 0xFFFFFFFFFFFFFFFF) << 32) |
                    ((block16e[2] & 0xFFFFFFFFFFFFFFFF) << 16) | ((block16e[1] & 0xFFFFFFFFFFFFFFFF)));
            }
            else
            {
                return (((block16e[1] & 0xFFFFFFFFFFFFFFFF) << 48) | ((block16e[2] & 0xFFFFFFFFFFFFFFFF) << 32) |
                    ((block16e[3] & 0xFFFFFFFFFFFFFFFF) << 16) | ((block16e[4] & 0xFFFFFFFFFFFFFFFF)));
            }

        }

        public static ushort function(ushort x, ushort y)
        {
            return (ushort)(x^(~(Cycle_shift_right(y,9))));
        }


        public static ushort Cycle_shift_right(ushort k, ushort i)
        {
            return (ushort)((k>>i)|(k<<(sizeof(ushort)*8 - i)));
        }
    }
}
