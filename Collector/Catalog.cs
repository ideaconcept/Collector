using System.Runtime.CompilerServices;

namespace Collector
{
    public class Catalog : CatalogBase
    {
        public override event CatalogAddDelegate CatalogAdded;
        public override event QuotationAddDelegate QuotationAdded;

        private const string fileNameCatalog = "catalog.txt";
        public Catalog(string id,
                       string name,
                       string year,
                       string publisher)
            : base(id, name, year, publisher)
        {
        }

        public static void ShowCatalog()
        {
            if (File.Exists(fileNameCatalog))
            {
                using (var reader = File.OpenText(fileNameCatalog))
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
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
