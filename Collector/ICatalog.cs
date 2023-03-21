using static Collector.CatalogBase;

namespace Collector
{
    internal interface ICatalog
    {
        public string ID { get; }
        public string Name { get; }
        public string Year { get; }
        public string Publisher { get; }

        void AddCatalog(string catalogData);

        public List<string[]> GetCatalogCoins();
        public List<string[]> GetCatalogOfYear(string fileName);
        public List<string[]> GetListOfCatalogs();

        event CatalogAddDelegate CatalogAdded;
    }
}
