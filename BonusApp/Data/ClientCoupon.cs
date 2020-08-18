using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BonusApp.Data
{
    public class ClientCoupon
    {
        [Required]
        public int Id { get; set; }

        public int ClientId { get; set; }
        public int CouponId { get; set; }
        public int SpentPages { get; set; }
        public string Date { get; set; }
        public Client Client { get; set; }
        public Coupon Coupon { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        public string TRCId
        {
            get
            {
                if (Id < 10)
                {
                    return $"TRCB000{Id}";
                }
                else if (Id >= 10 || Id < 100)
                {
                    return $"TRCB00{Id}";
                }
                else if (Id >= 100 || Id < 1000)
                {
                    return $"TRCB0{Id}";
                }
                else
                {
                    return $"TRCB{Id}";
                }
            }
        }
    }
}