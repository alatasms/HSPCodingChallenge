using ExpenseTrackerAPI.Domain.Entities;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExpenseTrackerAPI.Persistence.Context
{
    public class ExpenseTrackerAPIDBContext  : IdentityDbContext<User,Role,Guid>
    {
        public ExpenseTrackerAPIDBContext(DbContextOptions options): base(options)
        { 
        
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);


        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions);

            builder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(a => a.Accounts);

            builder.Entity<Account>()
                .HasOne(a => a.Currency);

            builder.Entity<Transaction>()
                .HasOne(t => t.Category);


            //SeedDatas

            builder.Entity<Currency>().HasData(
                new Currency() { Id = Guid.Parse("4197fd09-3938-4d50-872d-106249ee7854"), Name = "EUR" },
                new Currency() { Id = Guid.Parse("fab4f0d5-8187-4d54-92c2-eed0ce45d997"), Name = "TL" }
                );

            builder.Entity<Category>().HasData(
                new Category() { Id = Guid.Parse("0f0d1298-5e86-4964-b1f0-503059807b62"), Name = "Grocery" },
                new Category() { Id = Guid.Parse("e482a991-07aa-4af7-8bd7-3b65105be8dc"), Name = "Phone Bill" }
                );




        }

    }
}
