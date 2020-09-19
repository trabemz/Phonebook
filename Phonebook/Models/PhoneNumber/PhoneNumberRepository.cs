using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.PhoneNumber
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        PhonebookContext _context;
        public PhoneNumberRepository(PhonebookContext context)
        {
            _context = context;
        }

        public IQueryable<PhoneNumber> GetAll()
        {
            return _context.PhoneNumbers.AsQueryable();
        }
    }
}
