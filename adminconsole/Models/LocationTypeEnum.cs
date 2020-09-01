using System.ComponentModel.DataAnnotations;

namespace adminconsole.Models
{
    public enum LocationTypeEnum
    {
        [Display(Name = "ATM")]
        A = 0,
        [Display(Name = "Shared Branch")]
        S = 1
    }
}
