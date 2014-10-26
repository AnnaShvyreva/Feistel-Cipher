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
            ulong text = Convert.ToUInt64 (Console.ReadLine());
            //Console.WriteLine("Введите ключ");
            //ulong key = Convert.ToUInt64(Console.ReadLine());
            ulong key = 3456701234564567;

            int n = 10;

            Feistel_Cipher.Feistel(text, key, n);

                        
            Console.ReadKey();
        }
    }
}
