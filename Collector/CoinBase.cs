namespace Collector
{
    internal abstract class CoinBase : ICoin
    {
        public delegate void CatalogAddDelegate(object sender, EventArgs args);
        public abstract event CatalogAddDelegate CatalogAdded;

        public delegate void QuotationAddDelegate(object sender, EventArgs args);
        public abstract event QuotationAddDelegate QuotationAdded;

        public delegate void CollectionUpdateDelegate(object sender, EventArgs args);
        public abstract event CollectionUpdateDelegate CollectionUpdate;

        public CoinBase(string id, string name, int Denomination, string currency, int yearofrelease, int mintage, int diameter, int weight, string material)
        {
            this.ID = id;
            this.Name = name;
            this.Denomination = Denomination;
            this.Currency = currency;
            this.YearOfRelease = yearofrelease;
            this.Mintage = mintage;
            this.Diameter = diameter;
            this.Weight = weight;
            this.Material = material;
        }

        public string ID { get; }
        public string Name { get;  }
        public int Denomination { get; }
        public string Currency { get; }
        public int YearOfRelease { get; }
        public int Mintage { get; }
        public int Diameter { get; }
        public int Weight { get; }
        public string Material { get; }

        public abstract void AddQuanity(float grade);
        public abstract void AddQuanity(string grade);
        public abstract void AddQuotation(float grade);
        public abstract void AddQuotation(string grade);

        public abstract Statistics GetStatistics();

    }
}
