using System.ComponentModel.DataAnnotations;

namespace Phonebook.Models.Location
{
    public class Microdistrict : TerritorialUnit
    {
        [Display(Name = "Район")]
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }
    }
}
