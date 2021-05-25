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
        private readonly IApplicationDbContext db;


        public HistoryService(IApplicationDbContext db)
        {
            this.db = db;
        }

        public List<Transaction> GetTransactions(string userId)
        {
            var account = db.Accounts.Where(x => x.UserID == userId).FirstOrDefault();
            return db.Transactions.Where(x => x.ReceiverID.Equals(account.AccountID) || x.SenderID.Equals(account.AccountID)).ToList();
        }

        public void AddHistoryEntry(int senderId, int receiverId, DateTime date, string currencyType, float amount)
        {
            var entry = new Transaction(senderId, receiverId, date, currencyType, amount);
            db.Transactions.Add(entry);
            db.SaveChanges();
        }

    }
}
