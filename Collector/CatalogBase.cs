namespace Collector
{
    public abstract class CatalogBase : ICatalog
    {
        public delegate void CatalogAddDelegate(object sender, EventArgs args);
        public abstract event CatalogAddDelegate CatalogAdded;

        public delegate void QuotationAddDelegate(object sender, EventArgs args);
        public abstract event QuotationAddDelegate QuotationAdded;

        public CatalogBase(string id, string name, string year, string publisher)
        {
            this.ID = id;
            this.Name = name;
            this.Year = year;
            this.Publisher = publisher;
        }

        public string ID { get;  }
        public string Name { get; }
        public string Year { get; }
        public string Publisher { get; }

        public abstract List<string[]> GetCatalogCoins();
        public abstract List<string[]> GetCatalogOdYear(string fileName);

        public abstract void AddCatalog(string catalogData);
    }
}
