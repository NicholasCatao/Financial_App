using Financial_App.Domain.Enums;

namespace Financial_App.Domain.Request
{
    public class AccountFilter
    {
        public string AccountId { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinallDate { get; set; }
        public string MovimentType { get; set; }
    }
}
