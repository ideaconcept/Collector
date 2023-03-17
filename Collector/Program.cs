using Collector;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Xml.Linq;
using static Collector.CoinBase;

namespace Collector
{
    internal class Program
    {
        public const string fileNameCollection = "collection.txt";
        public const string fileNameCoinsList = "coins.txt";
        public const string fileNameCatalog = "catalog.txt";
        private static string nextNumber;

        static void CatalogAdded(object sender, EventArgs args)
        {
            Console.WriteLine("Dodano nowy katalog.\n");
        }
        static void QuotationAdded(object sender, EventArgs args)
        {
            Console.WriteLine("Dodano nową wycenę numizmatu.\n");
        }
        static void CollectionUpdate(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Zaktualizowano dane kolekcji numizmatów.\n");
            Console.ResetColor();
        }

        private static void Main()
        {
            var coin = new Coin("", "", 0, "", 0, 0, 0, "");
            var catalog = new Catalog("", "", "", "");

            coin.CollectionUpdate += CollectionUpdate;
            catalog.CatalogAdded += CatalogAdded;
            catalog.QuotationAdded += QuotationAdded;

            Implementation.Implementations();
            ShowMenu();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Twój wybór: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                string choice = Console.ReadLine();
                Console.ResetColor();

                if (choice == "1")
                {
                    ShowMenu();
                    Coin.ShowCollection(catalog.GetCatalogCoins());
                }
                else if (choice == "2")
                {
                    try
                    {
                        ShowMenu();

                        List<string[]> coinTable = catalog.GetCatalogCoins();
                        List<string[]> coinCollection = coin.GetCollection();

                        foreach (var record in coinCollection)
                        {
                            foreach (var recordsOfDictionary in coinTable)
                            {
                                if (record[0] == recordsOfDictionary[0])
                                {
                                    Console.Clear();
                                    ShowMenu();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nAktualizacja danych dotyczących ilości egzemplarzy poszczególnych numizmatów: \n");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Identyfikator:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{record[0]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Nazwa:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t\t{recordsOfDictionary[1]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Nominał:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[2]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Waluta:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t\t{recordsOfDictionary[3]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Rok wydania:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[4]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Średnica:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[5]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Waga:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t\t{recordsOfDictionary[6]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Materiał:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[7]}\n");
                                    Console.ResetColor();

                                    string newValue = ReadInputWithDefault(record[1], "Stan w ewidencji. Zatwierdź lub wprowadź aktualny stan: ");
                                    string oldRecord = record[0] + ";" + record[1];
                                    string newRecord = record[0] + ";" + newValue;

                                    Coin.ModifyCollectionRecord(fileNameCollection, oldRecord, newRecord);
                                    Console.Clear();
                                    ShowMenu();
                                }
                            }
                        }
                        if (CollectionUpdate != null)
                        {
                            CollectionUpdate(coin, new EventArgs());
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBag(e.Message);
                    }
                }
                else if (choice == "3") //Wyświetlenie zawartości katalogów wycen numizmatów
                {
                    try
                    {
                        ShowMenu();
                        Catalog.ShowCatalog();
                    }
                    catch (Exception e)
                    {
                        ShowBag(e.Message);
                    }
                }
                else if (choice == "4")
                {
                    try
                    {
                        Console.Clear();
                        ShowMenu();
                        Console.WriteLine("\nDodać opis czynności\n");
                        Console.WriteLine("Wprowadź dane katalogu: ");

                        string nameCatalog = GetDataFromUser("Nazwa: \t\t");
                        string yearCatalog = GetDataFromUser("Rok wydania: \t");
                        String publisherName = GetDataFromUser("Wydawca: \t");

                        Console.ResetColor();
                        Console.WriteLine("\n");

                        if (!string.IsNullOrEmpty(nameCatalog) && !string.IsNullOrEmpty(publisherName))
                        {
                            if ((int.TryParse(yearCatalog, out int result)))
                            {
                                string nextRecord = Catalog.RecordsOffCatalog().ToString();
                                nextNumber = nextRecord;
                                var record = nextRecord + ";" + nameCatalog + ";" + yearCatalog + ";" + publisherName;
                                catalog.AddCatalog(record);
                            }
                            else
                            {
                                Console.Clear();
                                ShowMenu();
                                ShowBag("Wprowadzony rok wydania katalogu nie jest cyfrą! Powtórz czynność dodana katalogu.\n");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            ShowMenu();
                            ShowBag("Wprowadzone dane katalogu nie mogą być puste! Powtórz czynność dodana katalogu.\n");
                        }
                        
                        string nameNewCatalog = "catalog_" + nextNumber + ".txt";
                        Catalog.AddCatalogFile(nameNewCatalog);

                        List<string[]> coinTable = catalog.GetCatalogCoins();
                        foreach (var recordsOfDictionary in coinTable)
                        {
                            
                            string newRecord = recordsOfDictionary[0] + ";0";

                            using (var writer = File.AppendText(nameNewCatalog))
                            {
                                writer.WriteLine(newRecord);
                            }
                        }

                        //Przeprowadzić aktualizację wyceny poszczególnych pozycji
                        
                        List<string[]> coinTableOfCatalog = catalog.GetCatalogOdYear(nameNewCatalog);

                        foreach (var record in coinTableOfCatalog)
                        {
                            foreach (var recordsOfDictionary in coinTable)
                            {
                                if (record[0] == recordsOfDictionary[0])
                                {
                                    Console.Clear();
                                    ShowMenu();
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nAktualizacja wyceny numizmatów dla nowego katalogu wyceny: \n");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Identyfikator:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{record[0]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Nazwa:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t\t{recordsOfDictionary[1]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Nominał:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[2]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Waluta:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t\t{recordsOfDictionary[3]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Rok wydania:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[4]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Średnica:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[5]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Waga:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t\t{recordsOfDictionary[6]}");

                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write("Materiał:");
                                    Console.ResetColor();
                                    Console.WriteLine($"\t\t{recordsOfDictionary[7]}\n");
                                    Console.ResetColor();

                                    string newValue = ReadInputWithDefault(record[1], "Wprowadź wycenę monety wg katalogu: ");
                                    string oldRecord = record[0] + ";" + record[1];
                                    string newRecord = record[0] + ";" + newValue;

                                    Coin.ModifyCollectionRecord(nameNewCatalog, oldRecord, newRecord);
                                    Console.Clear();
                                    ShowMenu();
                                }
                            }
                        }

                        //Console.Clear();
                        //ShowMenu();

                    }
                    catch (Exception e)
                    {
                        ShowBag(e.Message);
                    }
                }
                else if (choice == "5")
                {
                }
                else if (choice == "6")
                {
                }
                else if (choice == "7")
                {
                }
                else if (choice == "X" || choice == "x")
                {
                    Environment.Exit(0);
                }
                else
                {
                    ShowBag("Wprowadzono złą wartość. Wybierz: 1, 2, 3, 4, 5, 6, 7 lub X aby zakończyć pracę z programem.\n");
                }
            }
        }

        private static void ShowBag(string bag)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWystąpił błąd: {bag}");
            Console.ResetColor();
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("                  Witamy w programie Kolekcjoner:");
            Console.WriteLine("====================================================================");
            Console.WriteLine("Wybierz jedną z poniższych opcji lub X aby zakończyć pracę programu:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   1. Wyświetl zasób posiadanej kolekcji");
            Console.WriteLine("   2. Zaktualizuj ilość posiadanych numizmatów");
            Console.WriteLine("   3. Wyświetl listę dostępnych katalogów wycen");
            Console.WriteLine("   4. Wprowadź nowy katalog wyceny monet");
            Console.ResetColor();
            Console.Write("   5. Oblicz wartość kolekcji");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" (Niekatywne)");
            Console.ResetColor();
            Console.Write("   6. Wyświetl informacje nt. zmian wartości kolekcji");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" (Niekatywne)");
            Console.ResetColor();
            Console.Write("   7. Wyświetl dane statystyczne nt. posiadanych monet");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" (Niekatywne)");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   X. Zakończ pracę programu");
            Console.ResetColor();
            Console.WriteLine("====================================================================");
        }

        private static string GetDataFromUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            string userInput = Console.ReadLine();
            return userInput;
        }

        private static string ReadInputWithDefault(string defaultValue, string caret = "> ")
        {
        Etykieta:

            Console.ForegroundColor = ConsoleColor.Yellow;

            List<char> buffer = defaultValue.ToCharArray().Take(Console.WindowWidth - caret.Length - 1).ToList();
            Console.Write(caret);
            Console.Write(buffer.ToArray());
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(Math.Max(Console.CursorLeft - 1, caret.Length), Console.CursorTop);
                        break;
                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(Math.Min(Console.CursorLeft + 1, caret.Length + buffer.Count), Console.CursorTop);
                        break;
                    case ConsoleKey.Home:
                        Console.SetCursorPosition(caret.Length, Console.CursorTop);
                        break;
                    case ConsoleKey.End:
                        Console.SetCursorPosition(caret.Length + buffer.Count, Console.CursorTop);
                        break;
                    case ConsoleKey.Backspace:
                        if (Console.CursorLeft <= caret.Length)
                        {
                            break;
                        }
                        var cursorColumnAfterBackspace = Math.Max(Console.CursorLeft - 1, caret.Length);
                        buffer.RemoveAt(Console.CursorLeft - caret.Length - 1);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorColumnAfterBackspace, Console.CursorTop);
                        break;
                    case ConsoleKey.Delete:
                        if (Console.CursorLeft >= caret.Length + buffer.Count)
                        {
                            break;
                        }
                        var cursorColumnAfterDelete = Console.CursorLeft;
                        buffer.RemoveAt(Console.CursorLeft - caret.Length);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorColumnAfterDelete, Console.CursorTop);
                        break;
                    default:
                        var character = keyInfo.KeyChar;
                        if (character < 32)
                            break;
                        var cursorAfterNewChar = Console.CursorLeft + 1;
                        if (cursorAfterNewChar > Console.WindowWidth || caret.Length + buffer.Count >= Console.WindowWidth - 1)
                        {
                            break;
                        }
                        buffer.Insert(Console.CursorLeft - caret.Length, character);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorAfterNewChar, Console.CursorTop);
                        break;
                }
                keyInfo = Console.ReadKey(true);
            }

            if (float.TryParse(new string(buffer.ToArray()), out float result))
            {
                if (result < 0)
                {
                    ShowBag("Wprowadzona ilość musi być równa lub większa od 0 (zera).\n");
                    goto Etykieta;
                }
            }
            else
            {
                ShowBag("Wprowadzona ilość nie jest dopuszczalną wartością lub nie jest cyfrą.\n");
                goto Etykieta;
            }
            Console.Write(Environment.NewLine);
            return new string(buffer.ToArray());
        }

        private static void RewriteLine(string caret, List<char> buffer)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth - 1));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(caret);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(buffer.ToArray());
            Console.ResetColor();
        }
    }
}