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
            //ulong text = Convert.ToUInt64 (Console.ReadLine());
            string text = Console.ReadLine();
            Console.WriteLine("Введите ключ");
            string key = Console.ReadLine();
            //string key = "3456701234564567";
            //ulong key = 3456701234564567;
            //Console.WriteLine("Ключ:" + key);
            //ulong key = 3456701234564567;
            byte[] text_mass = Encoding.Default.GetBytes(text);
            byte[] key_mass = Encoding.Default.GetBytes(key);
            

            int n = 10;

            //Feistel_Cipher.Feistel(text, key, n);
            Feistel_Cipher.Feistel(text_mass, key_mass, n);
                        
            Console.ReadKey();
        }
    }
}
