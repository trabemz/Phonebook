using Microsoft.EntityFrameworkCore;
using Phonebook.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Models
{
    public class PhonebookContext : DbContext
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options)
            : base(options)
        {
        }

        public DbSet<TerritorialUnit> Locations { get; set; }
        public DbSet<PhoneNumber.PhoneNumber> PhoneNumbers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<District>(entity => {
                entity.HasIndex(e => e.Name).IsUnique();
            });
            builder.Entity<Microdistrict>(entity => {
                    entity.HasIndex(e => e.Name).IsUnique();
            });
            builder.Entity<PhoneNumber.PhoneNumber>(entity => {
                entity.HasIndex(e => e.Number).IsUnique();
            });
            base.OnModelCreating(builder);
        }
    }
}
