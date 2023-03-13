namespace Collector
{
    public abstract class CoinBase : ICoin
    {
        public delegate void CollectionUpdateDelegate(object sender, EventArgs args);
        public abstract event CollectionUpdateDelegate CollectionUpdate;

        public CoinBase(string id, string name, int denomination, string currency, int yearofrelease, int diameter, int weight, string material)
        {
            this.ID = id;
            this.Name = name;
            this.Denomination = denomination;
            this.Currency = currency;
            this.YearOfRelease = yearofrelease;
            this.Diameter = diameter;
            this.Weight = weight;
            this.Material = material;
        }

        public string ID { get; }
        public string Name { get;  }
        public int Denomination { get; }
        public string Currency { get; }
        public int YearOfRelease { get; }
        public int Diameter { get; }
        public int Weight { get; }
        public string Material { get; }

        public abstract void AddQuanity(string id, float grade);
        public abstract void AddQuanity(string id, string grade);

        public abstract Statistics GetStatistics();

    }
}
