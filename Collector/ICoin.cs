using static Collector.CoinBase;

namespace Collector
{
    public interface ICoin
    {
        public string ID { get; }
        public string Name { get; }
        public int Denomination { get; }
        public string Currency { get;  }
        public int YearOfRelease { get; }
        public int Diameter { get; }
        public int Weight { get; }
        public string Material { get; }

        void ShowCoinDetails(string id, string name, string denomination, string currency, string yearOfRelease, string diameter, string weight, string material);

        public List<string[]> GetCollection();

        event CollectionUpdateDelegate CollectionUpdate;
    }
}
