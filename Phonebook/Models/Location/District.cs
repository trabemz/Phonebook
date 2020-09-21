using System.Collections.Generic;

namespace Phonebook.Models.Location
{
    public class District : TerritorialUnit
    {
        public virtual ICollection<Microdistrict> Microdistricts { get; set; }
    }
}
