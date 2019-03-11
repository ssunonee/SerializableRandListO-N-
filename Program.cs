using System;
using System.IO;

namespace t
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new ListRand();

            for (int i = 0; i < 10; i++)
                list.Add("AddLast" + new Random().Next(1000, 10000));

            for (int i = 0; i < 10; i++)
                list.AddFirst("AddFirst" + new Random().Next(1000, 10000));

            Console.WriteLine("Before");
            list.WriteList();
            string path = @"C:\Users\Slvtrn\Desktop\t\t\sttt";
            using (FileStream fs = File.Create(path))
            {
                list.Serialize(fs);
            }
            Console.WriteLine("\nAfter");
            list = new ListRand();
            using (FileStream fs = File.OpenRead(path))
            {
                list.Deserialize(fs);
            }
            
            list.WriteList();
            Console.ReadKey();
        }
    }
}
