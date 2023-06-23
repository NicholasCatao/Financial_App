using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Financial_App.Domain.Model
{
    public class MovementModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
        public string Type { get; set; }
        public DateTime Data { get; set; }

        public AccountModel Account { get; set; }
    }
}

