using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.Location
{
    public class Microdistrict : TerritorialUnit
    {
        [Display(Name = "Район")]
        public int? DistrictId { get; set; }
        public virtual District District { get; set; }
    }
}
