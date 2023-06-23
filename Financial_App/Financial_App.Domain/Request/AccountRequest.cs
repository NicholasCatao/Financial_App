namespace Financial_App.Domain.Request
{
    public class AccountRequest
    {
        public string Name { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Limit { get; set; }
    }
}
