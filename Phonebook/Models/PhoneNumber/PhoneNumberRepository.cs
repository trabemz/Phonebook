using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models.PhoneNumber
{
    public class PhoneNumberRepository : IPhoneNumberRepository
    {
        private readonly PhonebookContext _context;
        public PhoneNumberRepository(PhonebookContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckIfNumberUnique(int? id, string number)
        {
            PhoneNumber phoneByNumber = await _context.PhoneNumbers.FirstOrDefaultAsync(phone => phone.Number == number);
            return phoneByNumber == null ? true : id == phoneByNumber.ID;
        }

        public async Task<int> Create(PhoneNumber number)
        {
            var result = _context.PhoneNumbers.Add(number);
            await Update();
            return result.Entity.ID;
        }

        public async Task DeleteById(int Id)
        {
            var model = await GetById(Id);
            _context.PhoneNumbers.Remove(model);
            await Update();
        }

        public IQueryable<PhoneNumber> GetAll()
        {
            return _context.PhoneNumbers.AsQueryable();
        }

        public async Task<PhoneNumber> GetById(int Id)
        {
            var model = await _context.PhoneNumbers.FirstOrDefaultAsync(item => item.ID == Id);
            return model;
        }

        public async Task Update()
        {
            await _context.SaveChangesAsync();
        }

    }
}
