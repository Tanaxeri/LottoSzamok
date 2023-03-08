using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LottoSzamok
{
    internal class Program
    {
        static string forras = @"..\..\Az ötöslottó eddigi nyerőszámai.csv";
        static List<int> szamok = new List<int>();
        static void Main(string[] args)
        {
            Beolvas();
            var leastPulledNumbers = szamok
                .GroupBy(x => x)
                .OrderBy(g => g.Count())
                .Select(g => g.Key)
                .Take(5);

            Console.WriteLine("A 5 legkevésbé húzott szám: ");
            foreach (var number in leastPulledNumbers)
            {
                Console.Write(number + " ");
            }

            Console.WriteLine("\nProgram vége!");
            Console.ReadKey();
        }
        static void Beolvas()
        {
            if (File.Exists(forras))
            {
                using (StreamReader sr = new StreamReader(forras))
                {
                    while (!sr.EndOfStream)
                    {
                        string ujSor = string.Empty;
                        while ((ujSor = sr.ReadLine()) != "" && ujSor != null)
                        {
                            string[] sor = ujSor.Split(';');
                            int[] s = new int[5];
                            for (int i = 0; i < 5; i++)
                            {
                                if (int.TryParse(sor[sor.Length - 5 + i], out int szam))
                                {
                                    s[i] = szam;
                                }
                                else
                                {
                                    Console.WriteLine($"Hiba: {sor[sor.Length - 5 + i]} nem egy érvényes egész szám.");
                                }
                            }

                            szamok.AddRange(s);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Nem található a forrás fájl!");
                Environment.Exit(0);
            }
        }

    }
}
