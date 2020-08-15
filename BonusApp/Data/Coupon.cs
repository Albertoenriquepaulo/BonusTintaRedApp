using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class Coupon
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Pages { get; set; }
        public ICollection<ClientCoupon> ClientCoupons { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
