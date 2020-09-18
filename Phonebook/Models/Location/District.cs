using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.Location
{
    public class District : TerritorialUnit
    {
        public virtual ICollection<Microdistrict> Microdistricts { get; set; }
    }
}
