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

        public static void ShowCatalog()
        {
            if (File.Exists(Program.fileNameCatalog))
            {
                using (var reader = File.OpenText(Program.fileNameCatalog))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nWykaz dostępnych katalogów wycen monet:\n\n");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\tLp.\tNazwa\t\t\t\t\tRok\tWydawca\n");
                    Console.WriteLine(("\t").PadRight(65, '-'));
                    Console.ResetColor();

                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var record = line.Split(';');
                        foreach (var kv in record)
                        {
                            Console.Write($"\t{kv}");
                        }
                        line = reader.ReadLine();
                        Console.WriteLine("");
                    }
                    Console.WriteLine("\n");
                }
            }
        }

        public override void AddCatalog(string catalogData)
        {
            if (File.Exists(Program.fileNameCatalog))
            {
                using (var writer = File.AppendText(Program.fileNameCatalog))
                {
                    writer.WriteLine(catalogData);
                    writer.WriteLineAsync();
                }
            }
            if (CatalogAdded != null)
            {
                CatalogAdded(this, new EventArgs());
            }
        }

        public override void AddQuotation(float quotation)
        {
            if (QuotationAdded != null)
            {
                QuotationAdded(this, new EventArgs());
            }
        }

        public override void AddQuotation(string quotation)
        {
            if (float.TryParse(quotation, out float result))
            {
                this.AddQuotation(result);
            }
            else
            {
                throw new Exception("Wprowadzona ilość nie jest dopuszczalną wartością.\n");
            }

            if (CatalogAdded != null)
            {
                CatalogAdded(this, new EventArgs());
            }
        }
    }
}
