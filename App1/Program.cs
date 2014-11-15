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
            byte[] text_mass = Encoding.Default.GetBytes(Console.ReadLine());
            Console.WriteLine("Введите ключ(не более 8 символов)");
            
            byte[] key_mass = Encoding.Default.GetBytes(Console.ReadLine());      

            //количество раундов
            int n = 10;
            //генерация массива ключей
            Key.Key_Gen(key_mass, n);
            //Hash.round_key_mass.Add(key_mass);
            Hash.AddRoundKey(key_mass, 0);
            
            Feistel_Cipher.Feistel(text_mass, n);

            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
