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
            //если текст не кратен 64 битам            
            if (text_mass.Length % 8 != 0)
            {
                byte[] temp = new byte[text_mass.Length + (8 - text_mass.Length%8)];
                Array.Copy(text_mass, 0, temp, temp.Length - text_mass.Length, text_mass.Length);
                text_mass = temp;
            }
                        
            /*Console.Write("Открытый текст: ");
            foreach (byte t in text_mass)
            {
                Console.Write(t+ " ");
            }
            Console.WriteLine();*/

            List<byte[]> feistel= new List<byte[]>{};

            //byte[] iv = new byte[8] { 11, 11, 11, 11, 11, 11, 11, 11 }; // вектор инициализации - режим CBC

            //делим на блоки и шифруем поблочно
            int count = -1;

            for (int i = 0; i < text_mass.Length; i+=8)
            {
                byte[] mass = new byte[8];
                
                Array.Copy(text_mass, i, mass, 0 ,8);

                /*Console.Write("Блок номер "+i/8 + ": ");
                foreach (byte t in mass)
                {
                    Console.Write(t + " ");
                }
                Console.WriteLine();

                Console.WriteLine("Начинаем шифровать");*/

                //if (i != 0) mass = BitConverter.GetBytes(BitConverter.ToUInt64(mass, 0) ^ BitConverter.ToUInt64(feistel[i - 1], 0)); //режим CBC
                //else mass = BitConverter.GetBytes(BitConverter.ToUInt64(mass, 0) ^ BitConverter.ToUInt64(iv, 0)); //режим CBC

                for (int r = 0; r < n; r++)
                {
                    count++;
                    //mass = Round(mass, Key.ReturnRoundKey(r), r);
                    mass = Round(mass, Hash.ReturnRoundKey(count), r);
                    
                    /*Console.Write(r + ": ");
                    foreach (byte t in mass)
                    {
                        Console.Write(t + " ");
                    }
                    Console.WriteLine();*/
                }
                feistel.Add(mass);
            }

            /*Console.Write("Зашифрованное сообщение: ");

            foreach (byte[] aFeistel in feistel)
            {
                foreach (byte t in aFeistel)
                {
                    Console.Write(t + " ");
                }
            }*/

            /*foreach (byte[] aFeistel in feistel)
            {
                Console.Write(Encoding.Default.GetString(aFeistel));
            }

            Console.WriteLine();*/
            
            List<byte[]> feistel_en = new List<byte[]> { };
            //int count = 0;

            for (int i = 0; i < text_mass.Length; i += 8)
            {
                byte[] mass = new byte[8];
                mass = feistel[i/8];
                /*Console.Write("Блок номер " + count + ": ");
                count++;
                foreach (byte t in aFeistel)
                {
                    Console.Write(t + " ");
                }
                Console.WriteLine();

                Console.WriteLine("Начинаем расшифровывать");*/
                for (int r = 0; r < n ; r++)
                {                                       
                    //mass = RoundEn(mass, Key.ReturnRoundKey(n-r-1), r);
                    mass = RoundEn(mass, Hash.ReturnRoundKey((i/8 + 1)*10 - 1 - r), r);

                    /*Console.Write(r + ": ");
                    foreach (byte t in mass)
                    {
                        Console.Write(t + " ");
                    }
                    Console.WriteLine();*/
                }

                //if (i != 0) mass = BitConverter.GetBytes(BitConverter.ToUInt64(mass, 0) ^ BitConverter.ToUInt64(feistel_en[i - 1], 0)); // режим CBC
                //else mass = BitConverter.GetBytes(BitConverter.ToUInt64(mass, 0) ^ BitConverter.ToUInt64(iv, 0)); // режим CBC

                feistel_en.Add(mass);
            }

            /*Console.Write("Расшифрованное сообщение: ");

            foreach (byte[] aFeistel in feistel_en)
            {
                foreach (byte t in aFeistel)
                {
                    Console.Write(t + " ");
                }
            }
            
            Console.WriteLine();*/

            Hash.hash_mass.Add(Hash.ReturnRoundKey(Hash.round_key_mass.Count - 1)); //добавляет значение хеш функции
            /*
            Console.Write("Расшифрованное сообщение: ");
            foreach (byte[] aFeistel in feistel_en)
            {
                Console.Write(Encoding.Default.GetString(aFeistel));
            }
             
            Console.WriteLine();*/
        }

        //зашифровка

        public static byte[] Round(byte[] block, byte[] key, int r) 
        {
            byte[] block_ = new byte[8];
            Array.Copy(block,0,block_,0,8);
            
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

            Hash.AddRoundKey(BitConverter.GetBytes(BitConverter.ToUInt16(block,0) ^ BitConverter.ToUInt16(block_,0)), r+1);

            return block;

        }

        //расшифровка

        public static byte[] RoundEn(byte[] block, byte[] key, int r) 
        {
            byte[] block1 = new byte[2];
            byte[] block2 = new byte[2];
            byte[] block3 = new byte[2];
            byte[] block4 = new byte[2];

            Array.Copy(block, 0, block1, 0, 2);
            Array.Copy(block, 2, block2, 0, 2);
            Array.Copy(block, 4, block3, 0, 2);
            Array.Copy(block, 6, block4, 0, 2);

            if (r != 0)
            {
                Array.Copy(block4, 0, block, 0, 2);
                Array.Copy(block3, 0, block, 2, 2);
                Array.Copy(BitConverter.GetBytes(BitConverter.ToUInt16(function(block4, block3), 0) ^
                    BitConverter.ToUInt16(block2, 0)), 0, block, 4, 2);
                Array.Copy(BitConverter.GetBytes(BitConverter.ToUInt16(block2, 0) ^ (BitConverter.ToUInt16(block1, 0)
                    ^ BitConverter.ToUInt16(key, 0))), 0, block, 6, 2);

            }
            else
            {
                Array.Copy(block1, 0, block, 0, 2);
                Array.Copy(block2, 0, block, 2, 2);
                Array.Copy(BitConverter.GetBytes(BitConverter.ToUInt16(function(block1, block2), 0) ^
                    BitConverter.ToUInt16(block3, 0)), 0, block, 4, 2);
                Array.Copy(BitConverter.GetBytes(BitConverter.ToUInt16(block3, 0) ^ (BitConverter.ToUInt16(block4, 0)
                    ^ BitConverter.ToUInt16(key, 0))), 0, block, 6, 2);
            }
            return block;

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
