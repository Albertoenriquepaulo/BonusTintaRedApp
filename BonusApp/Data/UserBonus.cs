using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class UserBonus
    {
        [Required]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BonusId { get; set; }
        public int SpentPages { get; set; }
        public string Date { get; set; }
        public User User { get; set; }
        public Bonus Bonus { get; set; }
        public ICollection<BonusSpending> BonusesSpending { get; set; }
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
