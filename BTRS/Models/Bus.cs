using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Bus
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Required")]
        public string captain_name { get; set; }

        [Required(ErrorMessage = "Required")]
        public int numOfSeats { get; set; }

        [ForeignKey("tripID")]
        public Trip trip { get; set; }


        [ForeignKey("administratorID")]
        public Administrators administrators { get; set; }


    }
}
