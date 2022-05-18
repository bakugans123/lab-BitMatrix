using System;
using System.Collections.Generic;

namespace ConsoleApp21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*string s = @"1";
            var lines = s.Split('\n');
            int rows = lines.Length;
            int columns = lines[0].Length;
            int strlen = s.Length - ((rows - 1) * 2);
            Console.WriteLine("Rows " + rows);
            Console.WriteLine("Strlen " + strlen);
            Console.WriteLine("Columns " + columns);
            Console.WriteLine();*/


            string s = @"1111
0000
1100";
            Console.WriteLine(BitMatrix.Parse(s));

            s = @"11
00
11";
            Console.WriteLine(BitMatrix.Parse(s));

            s = @"1";
            Console.WriteLine(BitMatrix.Parse(s));

            s = @"1101";
            Console.WriteLine(BitMatrix.Parse(s));
        }
    }
}
