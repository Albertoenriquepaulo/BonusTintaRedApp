using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonusApp.Data.RadzenGridMapper
{
    public class RadzenClientCoupon
    {
        public int Index { get; set; }
        public string TRCId { get; set; }
        public string CouponName { get; set; }
        public string ClientEmail { get; set; }
        public int CouponPages { get; set; }
        public int SpentPages { get; set; }
        public int RemainingPages { get; set; }
        public ClientCoupon ClientCoupon { get; set; }
    }
}
