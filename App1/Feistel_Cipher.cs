using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    class Feistel_Cipher
    {
        public static void Feistel(byte[] text_mass, int n)
        {
            //List<ushort> round_key = Key.Key_Gen(key, n);
            //List<ulong> block64 = new List<ulong> { };
            //List<ulong> block64e = new List<ulong> { };


            //если текст не кратен 64 битам            
            if (text_mass.Length % 8 != 0)
            {
                byte[] temp = new byte[text_mass.Length + (8 - text_mass.Length%8)];
                Array.Copy(text_mass, 0, temp, temp.Length - text_mass.Length, text_mass.Length);
                text_mass = temp;
            }

            //Console.WriteLine("Открытый текст: " + BitConverter.ToUInt64(text_mass, 0));
            Console.Write("Открытый текст: ");
            foreach (byte t in text_mass)
            {
                Console.Write(t+ " ");
            }
            Console.WriteLine();

            //byte[] mass = new byte[8];
            List<byte[]> feistel= new List<byte[]>{};

            //делим на блоки и шифруем поблочно

            for (int i = 0; i < text_mass.Length; i+=8)
            {
                byte[] mass = new byte[8];
                Array.Copy(text_mass, i, mass, 0 ,8);

                Console.Write("Блок номер "+i/8 + ": ");
                foreach (byte t in mass)
                {
                    Console.Write(t + " ");
                }
                Console.WriteLine();

                Console.WriteLine("Начинаем шифровать");
                for (int r = 0; r < n; r++)
                {
                    mass = Round(mass, Key.ReturnRoundKey(r), r);

                    Console.Write(r + ": ");
                    foreach (byte t in mass)
                    {
                        Console.Write(t + " ");
                    }
                    Console.WriteLine();
                }
                feistel.Add(mass);
            }

            Console.Write("Зашифрованное сообщение: ");

            foreach (byte[] aFeistel in feistel)
            {
                //Console.WriteLine(BitConverter.ToUInt16(BitConverter.GetBytes(aFeistel),0));
                foreach (byte t in aFeistel)
                {
                    Console.Write(t + " ");
                }
                //Console.WriteLine();
            }

            Console.WriteLine();


            List<byte[]> feistel_en = new List<byte[]> { };
            int count = 0;
            
            foreach (byte[] aFeistel in feistel)
            {
                byte[] mass = new byte[8];
                mass = aFeistel;
                Console.Write("Блок номер " + count + ": ");
                count++;
                foreach (byte t in aFeistel)
                {
                    Console.Write(t + " ");
                }
                Console.WriteLine();

                Console.WriteLine("Начинаем расшифровывать");
                for (int r = 0; r < n ; r++)
                {
                    mass = Round(mass, Key.ReturnRoundKey(n-r-1), r);

                    Console.Write(r + ": ");
                    foreach (byte t in mass)
                    {
                        Console.Write(t + " ");
                    }
                    Console.WriteLine();
                }
                feistel_en.Add(mass);
            }

            Console.Write("Расшифрованное сообщение: ");

            foreach (byte[] aFeistel in feistel_en)
            {
                //Console.Write(Encoding.Default.GetString(BitConverter.GetBytes(aFeistel)));
                //Console.WriteLine(BitConverter.ToUInt16(BitConverter.GetBytes(aFeistel), 0));
                foreach (byte t in aFeistel)
                {
                    Console.Write(t + " ");
                }
                //Console.WriteLine();
            }
            
            Console.WriteLine();

            Console.Write("Расшифрованное сообщение: ");
            foreach (byte[] aFeistel in feistel_en)
            {
                Console.Write(Encoding.Default.GetString(aFeistel));
            }
             
            Console.WriteLine();

                /*
                ulong tmp = 0;
                for (int l = 0; l < text_mass.Length; l++)
                {                
                    if ((Convert.ToUInt64(tmp) < sizeof(ulong))&&(tmp!=0))
                    {
                        tmp = tmp + text_mass[l];                    
                    }
                    else
                    {
                        block64.Add(Convert.ToUInt64(tmp));
                        tmp = 0;
                    }
                }
                
                foreach (ushort aBlock64 in block64)
                {
                    ulong buf = aBlock64;
                    for (int i = 0; i < n - 1; i++)
                    {
                        buf = Round(buf, round_key[i], 0);
                    }
                    block64e.Add(Round(buf, round_key[n - 1], 1));
                }

            foreach (ulong aBlock64e in block64e)
            {
                Console.Write(aBlock64e);

            }*/
        }

        public static byte[] Round(byte[] block, byte[] key, int r)
        {
            byte[] block1 = new byte[2];
            byte[] block2 = new byte[2];
            byte[] block3 = new byte[2];
            byte[] block4 = new byte[2];

            Array.Copy(block, 0, block1, 0, 2);
            Array.Copy(block, 2, block2, 0, 2);
            Array.Copy(block, 4, block3, 0, 2);
            Array.Copy(block, 6, block4, 0, 2);

            if (r != 9)
            {
                Array.Copy(block1, 0, block, 6, 2);
                Array.Copy(block2, 0, block, 4, 2);
                Array.Copy(BitConverter.GetBytes(BitConverter.ToUInt16(function(block1, block2),0) ^ 
                    BitConverter.ToUInt16(block3,0)), 0, block, 2, 2);
                Array.Copy(BitConverter.GetBytes((BitConverter.ToUInt16(function(block1, block2),0) ^ 
                    BitConverter.ToUInt16(block3,0)) ^ (BitConverter.ToUInt16(block4,0) ^ BitConverter.ToUInt16(key,0))), 
                    0, block, 0, 2);

            }
            else
            {
                Array.Copy(block1, 0, block, 0, 2);
                Array.Copy(block2, 0, block, 2, 2);
                Array.Copy(BitConverter.GetBytes(BitConverter.ToUInt16(function(block1, block2), 0) ^
                    BitConverter.ToUInt16(block3, 0)), 0, block, 4, 2);
                Array.Copy(BitConverter.GetBytes((BitConverter.ToUInt16(function(block1, block2), 0) ^
                    BitConverter.ToUInt16(block3, 0)) ^ (BitConverter.ToUInt16(block4, 0) ^ BitConverter.ToUInt16(key, 0))),
                    0, block, 6, 2);
            }            
            return block;
            /*List<ushort> block16 = new List<ushort> { };
            for (int i = 0; i < sizeof(ulong); i=i+sizeof(ushort))
            {
                block16.Add(Convert.ToUInt16((block << (i * sizeof(ushort))) >> 3 * sizeof(ushort)));

            }
            List<ushort> block16e = new List<ushort> { };
            block16e.Add(block16[0]);//4
            block16e.Add(block16[1]);//3
            block16e.Add((ushort)(function(block16[0], block16[1]) ^ block16[2]));//2
            block16e.Add((ushort)(block16e[2] ^ (block16[3] ^ key)));//1
            //соединить используя маску и сдвиги, метод возвращает этот ulong
            if (n == 0)
            {
                return (((block16e[3] & 0xFFFFFFFFFFFFFFFF) << 48) | ((block16e[2] & 0xFFFFFFFFFFFFFFFF) << 32) |
                    ((block16e[1] & 0xFFFFFFFFFFFFFFFF) << 16) | ((block16e[0] & 0xFFFFFFFFFFFFFFFF)));
            }
            else
            {
                return (((block16e[0] & 0xFFFFFFFFFFFFFFFF) << 48) | ((block16e[1] & 0xFFFFFFFFFFFFFFFF) << 32) |
                    ((block16e[2] & 0xFFFFFFFFFFFFFFFF) << 16) | ((block16e[3] & 0xFFFFFFFFFFFFFFFF)));
            } */

        }

        public static byte[] function(byte[] x, byte[] y)
        {
            return BitConverter.GetBytes(BitConverter.ToUInt16(y,0)^(~(Cycle_shift_right(BitConverter.ToUInt16(x,0),9))));
        }


        public static ushort Cycle_shift_right(ushort k, ushort i)
        {
            if (i > sizeof(ushort) * 8) i =(UInt16)(i % (sizeof(ushort) * 8));
            return (ushort)((k>>i)|(k<<(sizeof(ushort)*8 - i)));
        }
    }
}
