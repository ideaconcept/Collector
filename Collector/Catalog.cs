namespace Collector
{
    internal class Catalog : CatalogBase
    {
        public override event CatalogAddDelegate CatalogAdded;
        public override event QuotationAddDelegate QuotationAdded;
        
        public Catalog(string id,
                       string name,
                       string year,
                       string publisher)
            : base(id, name, year, publisher)
        {
        }

        public override void AddQuotation(float quotation)
        {
            if (QuotationAdded != null)
            {
                QuotationAdded(this, new EventArgs());
            }
        }

        public override void AddQuotation(string quotation)
        {
            if (float.TryParse(quotation, out float result))
            {
                this.AddQuotation(result);
            }
            else
            {
                throw new Exception("Wprowadzona ilość nie jest dopuszczalną wartością.\n");
            }

            if (CatalogAdded != null)
            {
                CatalogAdded(this, new EventArgs());
            }
        }
    }
}
