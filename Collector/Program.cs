using Collector;
using System;


var coin = new Coin("id1", "nazwa1", 100, "Złoty", 2016, 10, 10, 5, "CU");

var catalog = new Catalog("id1", "Katalog polskich monet obiegowych", "2015", "Fischer");

coin.CollectionUpdate += CollectionUpdate;

catalog.CatalogAdded += CatalogAdded;
catalog.QuotationAdded += QuotationAdded;


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
    Console.WriteLine("Zaktualoizowano dane kolekcji numizmatów.\n");
}




Console.WriteLine("                  Witamy w programie Kolekcjoner:");
Console.WriteLine("====================================================================\n");
Console.WriteLine("Wybierz jedną z poniższych opcji lub X aby zakończyć pracę programu:\n");
Console.WriteLine("   1. Wyświetl zasób posiadanej kolekcji (Niekatywne)");
Console.WriteLine("   2. Zaktualizuj ilość posiadanych numizmatów (Niekatywne)");
Console.WriteLine("   3. Wyświetl listę dostępnych katalogów wycen monet (Niekatywne)");
Console.WriteLine("   4. Wprowadź nowy katalog wyceny monet (Niekatywne)");
Console.WriteLine("   5. Oblicz wartość kolekcji (Niekatywne)");
Console.WriteLine("   6. Wyświetl informacje nt. zmian wartości kolekcji (Niekatywne)");
Console.WriteLine("   7. Wyświetl dane statystyczne nt. posiadanych monet (Niekatywne)");
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
        coin.AddQuanity(1);
    }
    else if (choice == "2")
    {
        catalog.AddQuotation(2);
    }
    else if (choice == "3")
    {
        catalog.AddQuotation(3);
    }
    else if (choice == "4") { }
    else if (choice == "5") { }
    else if (choice == "6") { }
    else if (choice == "7") { }
    else if (choice == "X" || (choice == "x"))
    {
        Environment.Exit(0);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Wprowadzono złą wartość. Wybierz: 1, 2, 3, 4, 5, 6, 7 lub x aby zakończyć pracę z programem.\n");
        Console.ResetColor();
    }
}