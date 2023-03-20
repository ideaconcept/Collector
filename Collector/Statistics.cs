using System.Diagnostics;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace Collector
{
    public class Statistics
    {
        public static void CollectionStatistics(List<string[]> coinCollection)
        {
            byte catalogNumber = ((byte)Catalog.RecordsInCatalog());
            catalogNumber = (byte)(catalogNumber - 1);
            string fileNamePriceList = "catalog_" + catalogNumber + ".txt";

            float numberOfCoins = 0;
            float amountOfCoins = 0;
            float value = 0;

            if (File.Exists(fileNamePriceList))
            {
                using (var reader = File.OpenText(fileNamePriceList))
                {
                    var line = reader.ReadLine();

                    while (line != null)
                    {
                        var record = line.Split(';');
                        foreach (var recordsOfDictionary in coinCollection)
                        {
                            if (record[0] == recordsOfDictionary[0])
                            {
                                if (recordsOfDictionary[1] != "0")
                                {
                                    value += float.Parse(recordsOfDictionary[1]) * float.Parse(record[1]);
                                    amountOfCoins += float.Parse(recordsOfDictionary[1]);
                                    numberOfCoins += 1;
                                }
                            }
                        }
                        line = reader.ReadLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("\n\tW Twojej kolekcji znajduje się ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{numberOfCoins}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" numizmatów w łacznej ilości ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{amountOfCoins}");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" egzemplarzy.\n");

                    Console.Write($"\tAktualna szacunkowa wartość Twojej kolekcji wynosi: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"{(String.Format("{0:C2}", value))}");
                    Console.ResetColor();
                }
            }
        }

        public static void ValueChanges(List<string[]> coinCollection, List<string[]> catalogTable)
        {
            List<string[]> valueChanges = new();

            foreach (var IDCatalog in catalogTable)
            {
                string fileName = "catalog_" + IDCatalog[0] + ".txt";
                string year = IDCatalog[2];

                if (File.Exists(fileName))
                {
                    float value = 0;
                    using (var reader = File.OpenText(fileName))
                    {
                        var line = reader.ReadLine();

                        while (line != null)
                        {
                            var record = line.Split(';');
                            foreach (var recordsOfDictionary in coinCollection)
                            {
                                if (record[0] == recordsOfDictionary[0])
                                {
                                    if (recordsOfDictionary[1] != "0")
                                    {
                                        value += float.Parse(recordsOfDictionary[1]) * float.Parse(record[1]);
                                    }
                                }
                            }
                            line = reader.ReadLine();
                        }

                        string records = year + ";" + value;
                        var dictionary = records.Split(';');
                        valueChanges.Add(dictionary);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nDane dotyczące zmiany wartości Twojej kolekcji:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t{0,-6} {1,18} {2,18} {3,12}", "Rok", "Wartość kolekcji", "Różnica wartości", "Zmiana w %");
            Console.WriteLine(("\t").PadRight(58, '-'));
            Console.ResetColor();

            float oldValue = 0;
            foreach (var x in valueChanges)
            {
                float valueFloat = float.Parse(x[1]);

                if (oldValue == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\t{0,-6} {1,18}", x[0], String.Format("{0:C2}", valueFloat));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\t{0,-6} {1,18} {2,18} {3,12:P2}", x[0], String.Format("{0:C2}", valueFloat), String.Format("{0:C2}", valueFloat - oldValue), (valueFloat - oldValue) / valueFloat);
                    Console.ResetColor();
                }
                oldValue = valueFloat;
            }
        }

        public static void ChangingValueCoins(int IDCoins, List<string[]> coinTable, List<string[]> catalogTable)
        {
            List<string[]> coinChanges = new();

            int counter = 1;

            foreach (var coin in coinTable)
            {
                if (counter == IDCoins)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\n\tIdentyfikator:");
                    Console.ResetColor();
                    Console.WriteLine($"\t{coin[0]}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tNazwa:");
                    Console.ResetColor();
                    Console.WriteLine($"\t\t{coin[1]}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tNominał:");
                    Console.ResetColor();
                    Console.WriteLine($"\t{coin[2]}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tWaluta:");
                    Console.ResetColor();
                    Console.WriteLine($"\t\t{coin[3]}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tRok wydania:");
                    Console.ResetColor();
                    Console.WriteLine($"\t{coin[4]}");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tŚrednica:");
                    Console.ResetColor();
                    Console.WriteLine("\t{0,-8:N2}", float.Parse(coin[5]));

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tWaga (g):");
                    Console.ResetColor();
                    Console.WriteLine("\t{0,-8:N2}", float.Parse(coin[6]));

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tMateriał:");
                    Console.ResetColor();
                    Console.WriteLine($"\t{coin[7]}\n");
                    Console.ResetColor();


                    foreach (var IDCatalog in catalogTable)
                    {
                        string fileName = "catalog_" + IDCatalog[0] + ".txt";
                        string year = IDCatalog[2];
                        string price = "";

                        if (File.Exists(fileName))
                        {
                            using (var reader = File.OpenText(fileName))
                            {
                                var line = reader.ReadLine();

                                while (line != null)
                                {
                                    var record = line.Split(';');

                                    if (record[0] == coin[0])
                                    {
                                        price = record[1];
                                    }
                                    line = reader.ReadLine();
                                }

                                string records = year + ";" + price;
                                var dictionary = records.Split(';');
                                coinChanges.Add(dictionary);
                            }
                        }
                    }
                }
                counter++;
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nDane dotyczące zmiany wartości numizmatu:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t{0,-6} {1,12} {2,18} {3,12}", "Rok", "Wartość", "Różnica wartości", "Zmiana w %");
            Console.WriteLine(("\t").PadRight(50, '-'));
            Console.ResetColor();

            float oldPrice = 0;
            foreach (var x in coinChanges)
            {
                float valueFloat = float.Parse(x[1]);

                if (oldPrice == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\t{0,-6} {1,12}", x[0], String.Format("{0:C2}", valueFloat));
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("\t{0,-6} {1,12} {2,18} {3,12:P2}", x[0], String.Format("{0:C2}", valueFloat), String.Format("{0:C2}", valueFloat - oldPrice), (valueFloat - oldPrice) / valueFloat);
                    Console.ResetColor();
                }
                oldPrice = valueFloat;
            }

        }
    }
}
