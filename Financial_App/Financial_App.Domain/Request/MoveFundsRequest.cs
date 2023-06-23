namespace Financial_App.Domain.Request
{
    public class MoveFundsRequest
    {
        public string AccountIdSource { get; set; }
        public string AccountIdDestination { get; set; }
        public decimal FundAmount { get; set; }
    }
}
