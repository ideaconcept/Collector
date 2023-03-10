namespace Collector
{
    public class Coin : CoinBase
    {
        public override event CollectionUpdateDelegate CollectionUpdate;
        
        public Coin(string id,
                    string name,
                    int Denomination,
                    string currency,
                    int yearofrelease,
                    int mintage,
                    int diameter,
                    int weight,
                    string material)
            : base(id, name, Denomination, currency, yearofrelease, mintage, diameter, weight, material)
        {
        }

        public override void AddQuanity(float quanity)
        {
            if (CollectionUpdate != null)
            {
                CollectionUpdate(this, new EventArgs());
            }
        }

        public override void AddQuanity(string quanity)
        {
            if (float.TryParse(quanity, out float result))
            {
                this.AddQuanity(result);
            }
            else
            {
                throw new Exception("Wprowadzona ilość nie jest dopuszczalną wartością.\n");
            }
        }

        public override Statistics GetStatistics()
        {
            var statistics = new Statistics();
            return statistics;
        }
    }
}
