using System.Runtime.CompilerServices;

namespace Collector
{
    public class Catalog : CatalogBase
    {
        public override event CatalogAddDelegate CatalogAdded;
        public override event QuotationAddDelegate QuotationAdded;

        public Catalog(string id,
                       string name,
                       string year,
                       string publisher)
            : base(id, name, year, publisher)
        {
        }

        public override void AddCatalog(string catalogData)
        {
            if (File.Exists(Program.fileNameCatalog))
            {
                using (var writer = File.AppendText(Program.fileNameCatalog))
                {
                    writer.WriteLine(catalogData);
                }
            }
            if (CatalogAdded != null)
            {
                CatalogAdded(this, new EventArgs());
            }
        }

        public static void AddCatalogFile(string name)
        {
            using (var writer = File.AppendText(name))
            {
            }
        }

        public override List<string[]> GetCatalogCoins()
        {
            List<string[]> coinTable = new();
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

        public override List<string[]> GetCatalogOfYear(string fileName)
        {
            List<string[]> coinTable = new();
            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
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

        public override List<string[]> GetListOfCatalogs()
        {
            List<string[]> catalogTable = new();
            if (File.Exists(Program.fileNameCatalog))
            {
                using (var reader = File.OpenText(Program.fileNameCatalog))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var dictionary = line.Split(';');
                        catalogTable.Add(dictionary);
                        line = reader.ReadLine();
                    }
                }
            }
            return catalogTable;
        }

        public static void ShowCatalog()
        {
            if (File.Exists(Program.fileNameCatalog))
            {
                using (var reader = File.OpenText(Program.fileNameCatalog))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nWykaz dostępnych katalogów wycen monet:\n\n");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("\t{0,-3} {1,-35} {2,6} {3,-10}", "Lp.", "Nazwa", "Rok ", "Wydawca");
                    Console.WriteLine(("\t").PadRight(65, '-'));
                    Console.ResetColor();

                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var record = line.Split(';');
                        Console.WriteLine("\t{0,-3} {1,-35} {2,6} {3,-10}", record[0], record[1], record[2], record[3]);
                        line = reader.ReadLine();
                    }
                }
            }
        }

        public static int RecordsInCatalog()
        {
            if (File.Exists(Program.fileNameCatalog))
            {
                using (var reader = File.OpenText(Program.fileNameCatalog))
                {

                    var records = 1;
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        records++;
                        line = reader.ReadLine();
                    }
                    return records;
                }
            }
            {
                return 0;
            }
        }

        public static void ShowCoins(List<string[]> coinTable)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nZawartość katalogu monet:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t{0, -3} {1,-13} {2,-35} {3,7} {4,-10} {5,8} {6,8} {7,8} {8,-10}", "Lp.", "Identyfikator", "Nazwa", "Nominał", "Waluta", "Rok wyd.", "Średnica", "Waga (g)", "Materiał");
            Console.WriteLine(("\t").PadRight(109, '-'));
            Console.ResetColor();

            int counter = 1;

            foreach (var recordsOfCoins in coinTable)
            {
                Console.WriteLine("\t{0, -3} {1,-13} {2,-35} {3,7} {4,-10} {5,8} {6,8} {7,8} {8,-10}", counter, recordsOfCoins[0], recordsOfCoins[1], recordsOfCoins[2], recordsOfCoins[3], recordsOfCoins[4], recordsOfCoins[5], recordsOfCoins[6], recordsOfCoins[7]);
                counter++;
            }
        }
    }
}
