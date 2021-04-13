using MyBank.Data;
using MyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly ApplicationDbContext db;

        public AccountsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int GetAccountId(string userId)
        {
            var account = db.Accounts.Where(x => x.UserID == userId).FirstOrDefault();
            return account.AccountID;
        }

        public string GetUserNameForAccountId(int accountId)
        {
            var account = db.Accounts.Where(x => x.AccountID == accountId).FirstOrDefault();
            var user = db.Users.Where(x => x.Id == account.UserID).FirstOrDefault();
            return user.UserName;
        }
    }
}
