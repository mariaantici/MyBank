using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBank.Data;
using MyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public class HistoryService : IHistoryService
    {
        private readonly ApplicationDbContext db;


        public HistoryService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Transaction> GetTransactions(string userId)
        {
            //var transaction = new Transaction(1, 1, DateTime.Now, "RON", 100);
            //db.Transactions.Add(transaction);
            //db.SaveChanges();

            var account = db.Accounts.Where(x => x.UserID == userId).FirstOrDefault();
            return db.Transactions.Where(x => x.ReceiverID.Equals(account.AccountID) || x.SenderID.Equals(account.AccountID)).ToList();
        }

    }
}
