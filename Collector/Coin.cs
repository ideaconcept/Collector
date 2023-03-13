using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

namespace Collector
{
    public class Coin : CoinBase
    {
        public override event CollectionUpdateDelegate CollectionUpdate;

        public List<string[]> coinTable = new List<string[]>();

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

            //Odczytania danych kolekcji, uzupełnienie ze słownika danych opisujących monety i wyświetlenie
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
                    Console.WriteLine("\n");
                }
            }
        }

        public override void AddQuanity(string id, float quanity)
        {
            if (CollectionUpdate != null)
            {
                CollectionUpdate(this, new EventArgs());
            }
        }

        public override void AddQuanity(string id, string quanity)
        {
            if (float.TryParse(quanity, out float result))
            {
                this.AddQuanity(id, result);
            }
            else
            {
                throw new Exception("Wprowadzona ilość nie jest dopuszczalną wartością lub nie jest cyfrą.\n");
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            return statistics;
        }

        public static void ModifyRecord(string fileName, string oldData, string newData)
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
        public static List<string[]> GetCatalog()
        {
            //Załadowanie słownika monet do listy
            List<string[]> coinTable = new List<string[]>();
            if (File.Exists(Program.fileNameCoinsList))
            {
                using (var reader = File.OpenText(Program.fileNameCoinsList))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var dictionary = line.Split(';');
                        coinTable.Add(dictionary);
                        line = reader.ReadLine();
                    }
                }
            }
            return coinTable;
        }
    }
}
