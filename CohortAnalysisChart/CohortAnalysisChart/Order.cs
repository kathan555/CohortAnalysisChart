namespace CohortAnalysisChart
{
    public class Order
    {
        public int InstrumentId
        {
            get;
            set;
        }

        public int DowgateOrderId
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public decimal Volume
        {
            get;
            set;
        }

        public bool? IsTradeable
        {
            get;
            set;
        } = null;


        public DateTime? TransactionTime
        {
            get;
            set;
        } = null;


        public string ClientOrderRef
        {
            get;
            set;
        } = string.Empty;


        public decimal? TotalTraderVolume
        {
            get;
            set;
        } = null;


        public int? TraderId
        {
            get;
            set;
        } = null;


        public string OrderChangeId
        {
            get;
            set;
        } = string.Empty;


        public string BookReference
        {
            get;
            set;
        } = string.Empty;


        public bool? DeletedBySystem
        {
            get;
            set;
        } = null;


    }
}
