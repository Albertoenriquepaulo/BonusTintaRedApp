﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
                    return $"TRC00{Id}";
                }
                else if (Id >= 10 || Id <= 100)
                {
                    return $"TRC0{Id}";
                }
                else
                {
                    return $"TRC{Id}";
                }
            }
        }
    }
}