using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleApiProject.Models
{
    public class Transaction : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransactionId { get; set; }
        public string TransactionName { get; set; }
        public decimal TransactionAmount { get; set; }
        public string ReceiverAccNumber { get; set; }
        public string Status {  get; set; }
        public string Reason { get; set; }
    }
}
