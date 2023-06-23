namespace Financial_App.Domain.Response
{
    public class AccountResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Limit { get; set; }
    }
}
