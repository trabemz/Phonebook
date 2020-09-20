using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.PhoneNumber
{
    public interface IPhoneNumberRepository
    {
        public IQueryable<PhoneNumber> GetAll();
        public Task<int> Create(PhoneNumber number);
        public Task<PhoneNumber> GetById(int Id);
        public Task Update();
        public Task DeleteById(int Id);
    }
}
