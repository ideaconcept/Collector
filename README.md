# Collector
Aaplikacja do prowadzenia ewidencji kolekcji numizmatów (monet kolekcjonerskich) oraz ich wyceny na podstawie danych pochodzących z uznanego katalogu wycen

Aplikacja umożliwia:
1. Przeglądanie stanu posiadanej kolekcji oraz informacji statystycznych dotyczących ilości numizmatów (unikalnych monet), ich łącznej ilości oraz wartości kolekcji wyliczonej na podstawie najnowszego katalogu wycen.
   Aplikacja wyświetla wyłącznie numizmaty dla których posiadana ilość jest większa niż 0.
2. AKtualizację stanu posiadania poszczególnych nimizmatów
3. Wyświetlenie dostępnych w aplikacji katalogów wycen
4. Dodanie nowego katalogu oraz wyceny monet dla zdefiniowanego zakresu słownikowego*
5. Analizę zmiany wartości posiadanej aktualnie kolekcji na podstawie cen z poszczególnych katalogów
6. Analizę zmian wartości poszczególnych numizmatów na podstawie cen z poszczególnych katalogów

Z uwagi iż program bazuje na pewnym zasobie dodano klasę Implementation, która podczas pierwszego uruchomienia tworzy pliki stanowiące zasób uruchomienia:
- słownik monet (8 pozyucji)
- 2 katalogi oraz ich wyceny dla ww. monet
- zasób kolekcji ze stanem posiadania 0

Projekt nie do końca oddaje model ze szkolenia. mam tu na myśli Employee -> supervisor (EmplyeeFile) oraz zwykły pracownik (EmployeeinMemory) i tym samym późniejsze wykorzystabnie klasy Statistics do zróżnicowanego użycia vs. charakter pracownika. Musiałbym zapewne utworzyć klasę Numismatic gdzie obok klasy Coins jakaś inna mogła dziedziczyć, np. dotycząca znaczków pocztowych (PostageStamp). Ale chciałem napisać program dla kolekcji monet :-)


P.S.
Napisałem kilka testów weryfikujących istnienie plików z zasobami oraz tworzenia na bazie ich zawartości list, które są przetwarzane w aplikacji. W testach podałem ścieżkę bezwzględną do miejsca ilokowania projektu na HDD. Test zwróci wynik negatywny w przypadku pobrania aplikacji do innej lokalizacji :-)
