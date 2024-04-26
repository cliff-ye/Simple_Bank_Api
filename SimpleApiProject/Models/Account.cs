using System.ComponentModel.DataAnnotations;

namespace SimpleApiProject.Models
{
    public class Account : BaseEntity
    {
        [Key]
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
    }
}
