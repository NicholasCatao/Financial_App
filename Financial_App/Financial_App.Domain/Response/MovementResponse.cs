namespace Financial_App.Domain.Response
{
    public class MovementResponse
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Data { get; set; }
    }
}
