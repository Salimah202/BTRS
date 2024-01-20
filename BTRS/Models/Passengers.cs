using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{

    [Index(nameof(Passengers.username), IsUnique = true)]
    public class Passengers
    {
        [Key]
        public int ID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Required")]
        public string username { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Required")]
        public string password { get; set; }

        public string email_address { get; set; }

        public string phone_number { get; set; }

        public string gender { get; set; }

        public ICollection<Passenger_Trip> passenger_trip { get; set; }
    }
}
