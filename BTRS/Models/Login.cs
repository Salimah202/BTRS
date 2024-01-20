using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Required")]
        public string username { set; get; }

        [Required(ErrorMessage = "Required")]
        public string password { set; get; }
    }
}
