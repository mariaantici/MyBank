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

        public List<Balance> GetBalancesForAccountId(int accountId)
        {
            return db.Balances.Where(x => x.AccountID.Equals(accountId)).ToList();
        }

        public Balance GetBalanceForAccountIdAndCurrency(int accountId, string currencyType)
        {
            return db.Balances.Where(x => x.AccountID.Equals(accountId) && x.Currency.Equals(currencyType)).FirstOrDefault();
        }
    }
}
