using Microsoft.EntityFrameworkCore;
using MyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Balance> Balances { get; set; }

        public DbSet<ApplicationUser> Users { get; set; }

        int SaveChanges();
    }
}
