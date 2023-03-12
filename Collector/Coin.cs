using System;
using System.Reflection.PortableExecutable;

namespace Collector
{
    public class Coin : CoinBase
    {
        public override event CollectionUpdateDelegate CollectionUpdate;

        private const string fileNameCollection = "collection.txt";
        private const string fileNameCoinsList = "coins.txt";

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

        public static void ShowCollection()
        {
            //Załadowanie słownika monet do listy
            List<string[]> coinTable = new List<string[]>();
            if (File.Exists(fileNameCoinsList))
            {
                using (var reader2 = File.OpenText(fileNameCoinsList))
                {
                    var line2 = reader2.ReadLine();
                    while (line2 != null)
                    {
                        var dictionary = line2.Split(';');
                        coinTable.Add(dictionary);
                        line2 = reader2.ReadLine();
                    }
                }
            }

            //Odczytania danych kolekcji, uzupełnienie danych opisujących monety ze słownika i wyświetlenie
            if (File.Exists(fileNameCollection))
            {
                using (var reader = File.OpenText(fileNameCollection))
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
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

        public override void AddQuanity(float quanity)
        {
            if (CollectionUpdate != null)
            {
                CollectionUpdate(this, new EventArgs());
            }
        }

        public override void AddQuanity(string quanity)
        {
            if (float.TryParse(quanity, out float result))
            {
                this.AddQuanity(result);
            }
            else
            {
                throw new Exception("Wprowadzona ilość nie jest dopuszczalną wartością.\n");
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            return statistics;
        }
    }
}
