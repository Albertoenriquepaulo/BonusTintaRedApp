using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BonusApp.Data
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Email { get; set; }
        public ICollection<UserBonus> UserBonus { get; set; }

        [NotMapped]
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
