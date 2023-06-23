using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Financial_App.Domain.Model
{
    public class AccountModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public Decimal Balance { get; set; }
        public Decimal Limit { get; set; }

        public ICollection<MovementModel> Movements { get; } = new List<MovementModel>();
    }
}
