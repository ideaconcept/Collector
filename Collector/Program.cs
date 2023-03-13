using Collector;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Collector
{
    internal class Program
    {
        public const string fileNameCollection = "collection.txt";
        public const string fileNameCoinsList = "coins.txt";
        public const string fileNameCatalog = "catalog.txt";

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
            Console.WriteLine("Zaktualizowano dane kolekcji numizmatów.\n");
        }

        private static void Main()
        {
            var coin = new Coin("", "", 0, "", 0, 0, 0, "");
            var catalog = new Catalog("", "", "", "");

            coin.CollectionUpdate += CollectionUpdate;
            catalog.CatalogAdded += CatalogAdded;
            catalog.QuotationAdded += QuotationAdded;

            Implementation.Implementations();
            Menu();

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
                    Menu();
                    Coin.ShowCollection(Coin.GetCatalog());
                }
                else if (choice == "2")
                {
                    try
                    {
                        //var identyfikator = "K(10)130";
                        //coin.AddQuanity(identyfikator, "Anna");

                        Menu();
                        List<string[]> coinTable = Coin.GetCatalog();

                        //Odczytania danych kolekcji, uzupełnienie ze słownika danych opisujących monety i wyświetlenie w celu aktualizacji danych
                        if (File.Exists(Program.fileNameCollection))
                        {
                            using (var reader = File.OpenText(Program.fileNameCollection))
                            {
                            //Dodać treść informcyjną

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
                                                Console.WriteLine("{0,-8} {1,-35} {2,7} {3,-10} {4,8} {5,8} {6,8} {7,-10} {8,4}", record[0], recordsOfDictionary[1], recordsOfDictionary[2], recordsOfDictionary[3], recordsOfDictionary[4], recordsOfDictionary[5], recordsOfDictionary[6], recordsOfDictionary[7], record[1]);
                                            }
                                        }
                                    }
                                    line = reader.ReadLine();
                                }
                                Console.WriteLine("\n");
                            }
                        }



                        //Coin.ModifyRecord(fileNameCollection, "OB(2)221;101", "OB(2)221;17");
                    }
                    catch (Exception e)
                    {
                        ShowBag(e.Message);
                    }
                }
                else if (choice == "3")
                {
                    try
                    {
                        Menu();
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
                        Console.WriteLine("\nDodać opis czynności\n");

                        Console.WriteLine("Wprowadź dane katalogu: ");

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Nazwa: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var nameCatalog = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Rok wydania: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var yearCatalog = Console.ReadLine();

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("Wydawca: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        var publisherName = Console.ReadLine();

                        Console.ResetColor();
                        Console.WriteLine("\n");

                        var record = nameCatalog + ";" + yearCatalog + ";" + publisherName;
                        catalog.AddCatalog(record);
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
                    string stan = "100";
                    ReadInputWithDefault(stan, "Moneta 1\nStan w ewidencji. Zatwierdź lub wprowadź aktualny stan: ");
                }
                else if (choice == "X" || choice == "x")
                {
                    Environment.Exit(0);
                }
                else
                {
                    ShowBag("Wprowadzono złą wartość. Wybierz: 1, 2, 3, 4, 5, 6, 7 lub x aby zakończyć pracę z programem.\n");
                }
            }
        }

        private static void ShowBag(string bag)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWystąpił błąd: {bag}");
            Console.ResetColor();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine("                  Witamy w programie Kolekcjoner:");
            Console.WriteLine("====================================================================");
            Console.WriteLine("Wybierz jedną z poniższych opcji lub X aby zakończyć pracę programu:\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   1. Wyświetl zasób posiadanej kolekcji");
            Console.ResetColor();
            Console.WriteLine("   2. Zaktualizuj ilość posiadanych numizmatów (Niekatywne)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   3. Wyświetl listę dostępnych katalogów wycen");
            Console.WriteLine("   4. Wprowadź nowy katalog wyceny monet");
            Console.ResetColor();
            Console.WriteLine("   5. Oblicz wartość kolekcji (Niekatywne)");
            Console.WriteLine("   6. Wyświetl informacje nt. zmian wartości kolekcji (Niekatywne)");
            Console.WriteLine("   7. Wyświetl dane statystyczne nt. posiadanych monet (Niekatywne)");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   X. Zakończ pracę programu");
            Console.ResetColor();
            Console.WriteLine("====================================================================\n");
         }

        public static string ReadInputWithDefault(string defaultValue, string caret = "> ")
        {
            Console.WriteLine();

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
                        if (character < 32) // not a printable chars
                            break;
                        var cursorAfterNewChar = Console.CursorLeft + 1;
                        if (cursorAfterNewChar > Console.WindowWidth || caret.Length + buffer.Count >= Console.WindowWidth - 1)
                        {
                            break; // currently only one line of input is supported
                        }
                        buffer.Insert(Console.CursorLeft - caret.Length, character);
                        RewriteLine(caret, buffer);
                        Console.SetCursorPosition(cursorAfterNewChar, Console.CursorTop);
                        break;
                }
                keyInfo = Console.ReadKey(true);
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
            Console.Write(buffer.ToArray());
        }
    }
}