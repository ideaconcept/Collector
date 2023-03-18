using System.Diagnostics;

namespace Collector
{
    public class Statistics
    {
        public int Quantity { get; private set; }
        public int NumberOfCopies { get; private set; }

        public Statistics()
        {
            this.Quantity = 0;
            this.NumberOfCopies = 0;
        }


        public static void CollectionValue(List<string[]> collectionTable)
        {
            byte counter = ((byte)Catalog.RecordsOffCatalog());
            counter = (byte)(counter - 1);
            string fileNamePriceList = "catalog_" + counter + ".txt";

            if (File.Exists(fileNamePriceList))
            {
                using (var reader = File.OpenText(fileNamePriceList))
                {
                    var line = reader.ReadLine();
                    float value = 0;
                    while (line != null)
                    {
                        var record = line.Split(';');
                        foreach (var recordsOfDictionary in collectionTable)
                        {
                            if (record[0] == recordsOfDictionary[0])
                            {
                                if (recordsOfDictionary[1] != "0")
                                {
                                    value = value + (float.Parse(recordsOfDictionary[1]) * float.Parse(record[1]));
                                }
                            }
                        }
                        line = reader.ReadLine();
                    }
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"\tAktualna szacunkowa wartość Twojej kolekcji wynosi: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"{(String.Format("{0:C2}", value))}");
                    Console.ResetColor();
                }
            }
        }
    }
}
