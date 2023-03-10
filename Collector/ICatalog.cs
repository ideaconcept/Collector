using static Collector.CatalogBase;

namespace Collector
{
    internal interface ICatalog
    {
        public string ID { get; }
        public string Name { get; }
        public string Year { get; }
        public string Publisher { get; }

        void AddQuotation(float grade);
        void AddQuotation(string grade);

        event CatalogAddDelegate CatalogAdded;
        event QuotationAddDelegate QuotationAdded;
    }
}
