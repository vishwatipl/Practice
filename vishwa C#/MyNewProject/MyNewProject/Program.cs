using System;
using System Text;

namespace MyApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("hello world");
            for (int i = 0; i < sb.Length; i++)
                Console.Write(sb[i]);
        }

    }
}
