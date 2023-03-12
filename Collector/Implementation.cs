﻿namespace Collector
{
    public class Implementation
    {
        private const string fileNameCatalog = "catalog.txt";
        private const string fileNameCollection = "collection.txt";
        private const string fileNameCoinsList = "coins.txt";

        //Opercja "wdrożeniowa" dla pierwszego uruchomienia funkcji aplikacji.
        public static void implementation()
        {
            //Dodanie do słownika katalogów jednego katalogu i jego wyceny.
            if (File.Exists(fileNameCatalog))
            {
            }
            else
            {
                using (var writer = File.AppendText(fileNameCatalog))
                {
                    writer.WriteLine("1;Katalog polskich monet obiegowych;2019;Fischer");
                }
                //Dopisać jeszcze kod dodania wycen dla pierwszego katalogu
            }

            //Utworzenie zasobu słownika monet
            if (File.Exists(fileNameCoinsList))
            {
            }
            else
            {
                using (var writer = File.AppendText(fileNameCoinsList))
                {
                    writer.WriteLine("K(10)130;Przewodnictwo Polski w Radzie UE;10;Złotych;2011;32;14,14;Ag 925");
                    writer.WriteLine("K(20)006;Pałac Królewski w Łazienkach;10;Złotych;1995;40;31,1;Ag 999");
                    writer.WriteLine("K(50)001;Władysław Jagiełło;50;Złotych;2015;45;62,2;Ag 999");
                    writer.WriteLine("OB(2)221;Poznań;2;Złote;2011;27;8,15;GN");
                }
            }

            //Utworzenie zasobu kolekcji
            if (File.Exists(fileNameCollection))
            {
            }
            else
            {
                //using (var writer = File.AppendText(fileNameCollection))
                //{

                //}
            }
        }
    }
}
