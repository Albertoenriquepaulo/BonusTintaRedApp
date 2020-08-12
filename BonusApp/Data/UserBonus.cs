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
        public DateTime Date { get; set; }
        public User User { get; set; }
        public Bonus Bonus { get; set; }
    }
}
