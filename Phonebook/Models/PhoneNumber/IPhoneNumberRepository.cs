using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.PhoneNumber
{
    public interface IPhoneNumberRepository
    {
        public IQueryable<PhoneNumber> GetAll();
    }
}
