using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonusApp.Data
{
    public class Client
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Email { get; set; }

        public ICollection<ClientCoupon> ClientCoupons { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}