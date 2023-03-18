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

        public override List<string[]> GetCatalogCoins()
        {
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

        public override List<string[]> GetCatalogOdYear(string fileName)
        {
            List<string[]> coinTable = new List<string[]>();
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
        
        public static int RecordsOffCatalog()
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

        public static void AddCatalogFile(string name)
        {
            using (var writer = File.AppendText(name))
            {
            }
        }
    }
}
