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

        void AddQuanity(string id, float grade);
        void AddQuanity(string id, string grade);

        event CollectionUpdateDelegate CollectionUpdate;

        Statistics GetStatistics();
    }
}
