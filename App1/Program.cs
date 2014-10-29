using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace App1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите открытый текст");
            string text = Console.ReadLine();
            Console.WriteLine("Введите ключ(8 символов)");
            string key = Console.ReadLine();
            byte[] text_mass = Encoding.Default.GetBytes(text);
            byte[] key_mass = Encoding.Default.GetBytes(key);
            //Console.WriteLine(Encoding.Default.GetString(text_mass));
            //Console.WriteLine(BitConverter.ToUInt64(key_mass,0));           

            int n = 10;

            Feistel_Cipher.Feistel(text_mass, key_mass, n);
                        
            Console.ReadKey();
        }
    }
}
