namespace Collector
{
    internal class Program
    {
        public const string fileNameCollection = "collection.txt";
        public const string fileNameCoinsList = "coins.txt";
        public const string fileNameCatalog = "catalog.txt";
        private static string? nextNumber;

        static void CatalogAdded(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDodano nowy katalog oraz wycenę numizmatów.\n");
            Console.ResetColor();
        }
        static void CollectionUpdate(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nZaktualizowano dane kolekcji numizmatów.\n");
            Console.ResetColor();
        }

        private static void Main()
        {
            var coin = new Coin("", "", 0, "", 0, 0, 0, "");
            var catalog = new Catalog("", "", "", "");
            catalog.CatalogAdded += CatalogAdded;
            coin.CollectionUpdate += CollectionUpdate;


            Implementation.Implementations();
            ShowMenu();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nTwój wybór: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                string choice = Console.ReadLine();
                Console.ResetColor();

                if (choice == "1")
                {
                    try
                    {
                        ShowMenu();
                        Coin.ShowCollection(catalog.GetCatalogCoins());
                        Statistics.CollectionStatistics(coin.GetCollection());
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
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

                                    coin.ShowCoinDetails(record[0], recordsOfDictionary[1], recordsOfDictionary[2], recordsOfDictionary[3], recordsOfDictionary[4], recordsOfDictionary[5], recordsOfDictionary[6], recordsOfDictionary[7]);
                                    Console.WriteLine();

                                    string newValue = ReadInputWithDefault(record[1], 0, "Stan w ewidencji. Zatwierdź lub wprowadź aktualny stan: ");

                                    string oldRecord = record[0] + ";" + record[1];
                                    string newRecord = record[0] + ";" + (byte)float.Parse(newValue);

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
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "3")
                {
                    try
                    {
                        ShowMenu();
                        Catalog.ShowCatalog();
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "4")
                {
                    try
                    {
                        Console.Clear();
                        ShowMenu();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\nNależy wprowadzić dane opisujące dodawany nowy katalog wyceny nmizmatów oraz w kolejnym kroku wycenę poszczególnych numizmatów zgodnie z informacją opublikowaną w katalogu.\n");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Wprowadź dane katalogu: ");

                        string nameCatalog = GetDataFromUser("Nazwa: \t\t");
                        string yearCatalog = GetDataFromUser("Rok wydania: \t");
                        string publisherName = GetDataFromUser("Wydawca: \t");

                        Console.ResetColor();
                        Console.WriteLine("\n");

                        if (!string.IsNullOrEmpty(nameCatalog) && !string.IsNullOrEmpty(publisherName))
                        {
                            if ((int.TryParse(yearCatalog, out int result)))
                            {
                                string nextRecord = Catalog.RecordsInCatalog().ToString();
                                nextNumber = nextRecord;
                                var record = nextRecord + ";" + nameCatalog + ";" + yearCatalog + ";" + publisherName;
                                catalog.AddCatalog(record);
                            }
                            else
                            {
                                Console.Clear();
                                ShowMenu();
                                ShowBug("Wprowadzony rok wydania katalogu nie jest cyfrą! Powtórz czynność dodana katalogu.\n");
                            }
                        }
                        else
                        {
                            Console.Clear();
                            ShowMenu();
                            ShowBug("Wprowadzone dane katalogu nie mogą być puste! Powtórz czynność dodana katalogu.\n");
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

                        List<string[]> coinTableOfCatalog = catalog.GetCatalogOfYear(nameNewCatalog);

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

                                    coin.ShowCoinDetails(record[0], recordsOfDictionary[1], recordsOfDictionary[2], recordsOfDictionary[3], recordsOfDictionary[4], recordsOfDictionary[5], recordsOfDictionary[6], recordsOfDictionary[7]);
                                    Console.WriteLine();

                                    string newValue = ReadInputWithDefault(record[1], 1, "Wprowadź wycenę monety wg katalogu: ");
                                    string oldRecord = record[0] + ";" + record[1];
                                    string newRecord = record[0] + ";" + newValue;

                                    Coin.ModifyCollectionRecord(nameNewCatalog, oldRecord, newRecord);
                                    Console.Clear();
                                    ShowMenu();
                                }
                            }
                        }
                        if (CatalogAdded != null)
                        {
                            CatalogAdded(coin, new EventArgs());
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "5")
                {
                    try
                    {
                        ShowMenu();
                        Statistics.ValueChanges(coin.GetCollection(), catalog.GetListOfCatalogs());
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "6")
                {
                    try
                    {
                        ShowMenu();
                        Catalog.ShowCoins(catalog.GetCatalogCoins());

                        while (true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("\nPodaj numer porządkowy monety dla której mają zostać wyświetlone informacje szczegółowe lub q aby zakończyć: ");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            string? choice2 = Console.ReadLine();
                            Console.ResetColor();

                            var liczbaRekordow = catalog.GetCatalogCoins().Count;

                            if (float.TryParse(choice2, out float result))
                            {
                                if ((int)result <= liczbaRekordow)
                                {
                                    Statistics.ChangingValueCoins((int)float.Parse(choice2), catalog.GetCatalogCoins(), catalog.GetListOfCatalogs());
                                }
                                else
                                {
                                    ShowBug("Wprowadzono złą wartość. Podaj numer porządkowy lub Q aby wrócić do menu głównego.\n");
                                }
                            }
                            else
                            {
                                if (choice2 == "q" || choice2 == "Q")
                                {
                                    Console.Clear();
                                    ShowMenu();
                                    break;
                                }
                                else
                                {
                                    ShowBug("Wprowadzono złą wartość. Podaj numer porządkowy lub Q aby wrócić do menu głównego.\n");
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "X" || choice == "x")
                {
                    Environment.Exit(0);
                }
                else
                {
                    ShowBug("Wprowadzono złą wartość. Wybierz: 1, 2, 3, 4, 5, 6 lub X aby zakończyć pracę z programem.\n");
                }
            }
        }

        private static void ShowBug(string bug)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWystąpił błąd: {bug}");
            Console.ResetColor();
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine("                  Witamy w programie Kolekcjoner:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
            Console.WriteLine("Wybierz jedną z poniższych opcji lub X aby zakończyć pracę programu:\n");
            Console.WriteLine("   1. Wyświetl informacje nt. posiadanej kolekcji i jej wartości");
            Console.WriteLine("   2. Zaktualizuj ilość posiadanych numizmatów");
            Console.WriteLine("   3. Wyświetl listę dostępnych katalogów wycen");
            Console.WriteLine("   4. Wprowadź nowy katalog wyceny monet");
            Console.WriteLine("   5. Wyświetl informacje nt. zmian wartości kolekcji");
            Console.WriteLine("   6. Wyświetl informacje nt. zmian wartości monet");
            Console.WriteLine("   X. Zakończ pracę programu");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
        }

        private static string GetDataFromUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            string userInput = Console.ReadLine();
            return userInput;
        }

        private static string ReadInputWithDefault(string defaultValue, int minimum, string caret = "> ")
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
                if (result < minimum)
                {
                    ShowBug($"Wprowadzona liczba musi być większa od {minimum}.\n");
                    goto Etykieta;
                }
            }
            else
            {
                ShowBug("Wprowadzona ilość nie jest dopuszczalną wartością lub nie jest cyfrą.\n");
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