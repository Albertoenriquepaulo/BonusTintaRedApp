using System;
using System.ComponentModel.DataAnnotations;

namespace BonusApp.Data
{
    public class Transaction
    {
        [Required]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public int CouponId { get; set; }
        public int ClientCouponId { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int SpentPages { get; set; }

        public string Date { get; set; }
        public Client Client { get; set; }
        public Coupon Coupon { get; set; }
        public ClientCoupon ClientCoupon { get; set; }
    }
}