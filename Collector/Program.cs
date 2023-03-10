







Console.WriteLine("                  Witamy w programie Kolekcjoner:");
Console.WriteLine("====================================================================\n");
Console.WriteLine("Wybierz jedną z poniższych opcji lub X aby zakończyć pracę programu:\n");
Console.WriteLine("   1. Wyświetl zasób posiadanej kolekcji (Niekatywne)");
Console.WriteLine("   2. Zaktualizuj ilość posiadanych numizmatów (Niekatywne)");
Console.WriteLine("   3. Wyświetl listę dostępnych katalogów wycen monet (Niekatywne)");
Console.WriteLine("   4. Wprowadź nowy katalog wyceny monet (Niekatywne)");
Console.WriteLine("   5. Zaktualizuj ilość posiadanych numizmatów (Niekatywne)");
Console.WriteLine("   6. Oblicz wartość kolekcji (Niekatywne)");
Console.WriteLine("   7. Wyświetl informacje nt. zmian wartości kolekcji (Niekatywne)");
Console.WriteLine("   8. Wyświetl dane statystyczne nt. posiadanych monet (Niekatywne)");
Console.WriteLine("   X. Zakończ pracę programu\n");


while (true)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write("Twój wybór: ");
    Console.ResetColor();

    Console.ForegroundColor = ConsoleColor.Magenta;
    var choice = Console.ReadLine();
    Console.ResetColor();

    if (choice == "1")
    {
        Environment.Exit(0);
    }
    else if (choice == "2") { }
    else if (choice == "3") { }
    else if (choice == "4") { }
    else if (choice == "5") { }
    else if (choice == "6") { }
    else if (choice == "7") { }
    else if (choice == "8") { }
    else if (choice == "X" || (choice == "x"))
    {
        Environment.Exit(0);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Wprowadzono złą wartość. Wybierz: 1, 2, 3, 4, 5, 6, 7, 8 lub x aby zakończyć pracę z programem.\n");
        Console.ResetColor();
    }
}