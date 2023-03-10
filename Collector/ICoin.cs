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
        public int Mintage { get; }
        public int Diameter { get; }
        public int Weight { get; }
        public string Material { get; }

        void AddQuanity(float grade);
        void AddQuanity(string grade);

        void AddQuotation(float grade);
        void AddQuotation(string grade);

 //       event CatalogAddDelegate CatalogAdded;
 //       event QuotationAddDelegate QuotationAdded;
 //       event CollectionUpdateDelegate CollectionUpdate;

        Statistics GetStatistics();
    }
}
