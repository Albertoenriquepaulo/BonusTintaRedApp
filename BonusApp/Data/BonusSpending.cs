using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class BonusSpending
    {
        [Required]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BonusId { get; set; }
        public int UserBonusId { get; set; }

        [Range(1, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int SpentPages { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Bonus Bonus { get; set; }
        public UserBonus UserBonus { get; set; }
    }
}
