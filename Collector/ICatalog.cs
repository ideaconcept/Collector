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
        public List<string[]> GetCatalogOdYear(string fileName);

        event CatalogAddDelegate CatalogAdded;
        event QuotationAddDelegate QuotationAdded;
    }
}
