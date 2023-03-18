using System.Diagnostics;

namespace Collector
{
    public class Statistics
    {
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
                    while (line != null)
                    {
                        var record = line.Split(';');
                        if (record[1] != "0")
                        {
                            foreach (var recordsOfDictionary in collectionTable)
                            {
                                if (record[0] == recordsOfDictionary[0])
                                {
                                    if (recordsOfDictionary[1] != "0")
                                    {
                                        float value = float.Parse(recordsOfDictionary[1]) * float.Parse(record[1]);
                                        
                                        
                                        
                                        
                                        
                                        Console.WriteLine("\t{0,-8} {1,7} {2,-8} {3,7}", record[0], record[1], recordsOfDictionary[0], recordsOfDictionary[1]);
                                        Console.WriteLine(value);
                                    }
                                }
                            }
                        }
                        line = reader.ReadLine();
                    }
                }
            }

        }
    }
}
