using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Trip
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string destination { get; set; }

        [Required(ErrorMessage = "Required")]
        public int bus_number { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime start_date { get; set; }
        
        [Required(ErrorMessage = "Required")]
        public DateTime end_date { get; set;}

        public ICollection<Passenger_Trip> passenger_trip { get; set; }

        [ForeignKey("administorID")]
        public Administrators administrators { get; set; }
        public ICollection<Bus> bus { get; set; }
  

    }
}
