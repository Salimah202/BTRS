using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Passenger_Trip
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("passengerID")]
        public Passengers passenger { get; set; }

        [ForeignKey("tripID")]
        public Trip trip { get; set; }

    }
}
