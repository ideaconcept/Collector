using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Collector
{
    public class Coin : CoinBase
    {
        public override event CollectionUpdateDelegate CollectionUpdate;

        //public List<string[]> coinTable = new();

        public Coin(string id,
                    string name,
                    int denomination,
                    string currency,
                    int yearofrelease,
                    int diameter,
                    int weight,
                    string material)
            : base(id, name, denomination, currency, yearofrelease, diameter, weight, material)
        {
        }

        public static void ShowCollection(List<string[]> coinTable)
        {
            if (File.Exists(Program.fileNameCollection))
            {
                using (var reader = File.OpenText(Program.fileNameCollection))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nZawartość Twojej kolekcji:\n\n");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t{0,-8} {1,-35} {2,7} {3,-10} {4,8} {5,8} {6,8} {7,-10} {8,4}", "ID", "Nazwa", "Nominał", "Waluta", "Rok wyd.", "Średnica", "Waga (g)", "Materiał", "Liczba");
                    Console.WriteLine(("\t").PadRight(110, '-'));
                    Console.ResetColor();

                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var record = line.Split(';');
                        if (record[1] != "0")
                        {
                            foreach (var recordsOfDictionary in coinTable)
                            {
                                if (record[0] == recordsOfDictionary[0])
                                {
                                    Console.WriteLine("\t{0,-8} {1,-35} {2,7} {3,-10} {4,8} {5,8} {6,8} {7,-10} {8,4}", record[0], recordsOfDictionary[1], recordsOfDictionary[2], recordsOfDictionary[3], recordsOfDictionary[4], recordsOfDictionary[5], recordsOfDictionary[6], recordsOfDictionary[7], record[1]);
                                }
                            }
                        }
                        line = reader.ReadLine();
                    }
                }
            }
        }

        public static void ModifyCollectionRecord(string fileName, string oldData, string newData)
        {
            int lineNumber = 0;
            bool traced = false;
            string[] textLine = System.IO.File.ReadAllLines(fileName);
            for (int i = 0; i < textLine.Length; i++)
            {
                if (textLine[i].Contains(oldData))
                {
                    traced = true;
                }
                if (traced is true)
                {
                    if (textLine[i].Contains(oldData))
                    {
                        textLine[i] = newData;
                        traced = false;
                        System.IO.File.WriteAllLines(fileName, textLine);
                        break;
                    }
                }
            }
        }

        public override void ShowCoinDetails(string id, string name, string denomination, string currency, string yearOfRelease, string diameter, string weight, string material)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Identyfikator:");
            Console.ResetColor();
            Console.WriteLine($"\t\t{id}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Nazwa:");
            Console.ResetColor();
            Console.WriteLine($"\t\t\t{name}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Nominał:");
            Console.ResetColor();
            Console.WriteLine($"\t\t{denomination}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Waluta:");
            Console.ResetColor();
            Console.WriteLine($"\t\t\t{currency}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Rok wydania:");
            Console.ResetColor();
            Console.WriteLine($"\t\t{yearOfRelease}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Średnica:");
            Console.ResetColor();
            Console.WriteLine($"\t\t{diameter}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Waga:");
            Console.ResetColor();
            Console.WriteLine($"\t\t\t{weight}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("Materiał:");
            Console.ResetColor();
            Console.WriteLine($"\t\t{material}\n");
            Console.ResetColor();
        }

        public override List<string[]> GetCollection()
        {
            List<string[]> coinCollection = new ();
            if (File.Exists(Program.fileNameCollection))
            {
                using (var reader = File.OpenText(Program.fileNameCollection))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var dictionary = line.Split(';');
                        coinCollection.Add(dictionary);
                        line = reader.ReadLine();
                    }
                }
            }
            return coinCollection;
        }
    }
}
