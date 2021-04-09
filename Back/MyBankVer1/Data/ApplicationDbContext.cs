using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBank.Models;
using MyBankVer1.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBankVer1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //AccountsTable
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Balance> Balances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.Receiver)
                .WithMany(t => t.ReceiveTransactions)
                .HasForeignKey(m => m.ReceiverID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(p => p.Sender)
                .WithMany(t => t.SendTransactions)
                .HasForeignKey(m => m.SenderID)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}
