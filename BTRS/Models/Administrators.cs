using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{

    [Index(nameof(Administrators.userName), IsUnique = true)]
    public class Administrators
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string userName { get; set; }

        [Required(ErrorMessage = "Required")]
        public string password { get; set; }

        [Required(ErrorMessage = "Required")]
        public string full_name { get; set; }

        public ICollection<Trip> trip { get; set; }

        public ICollection<Bus> bus { get; set; }
    }
}
