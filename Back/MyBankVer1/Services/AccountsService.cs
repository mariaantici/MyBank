using Microsoft.AspNetCore.Identity;
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
        public const string Enel = "Enel";
        public const int EnelId = 1000;
        public const string Cez = "Cez";
        public const int CezId = 1001;
        public const string Vodafone = "Vodafone";
        public const int VodafoneId = 1002;
        public const string Orange = "Orange";
        public const int OrangeId = 1003;
        public const string Digi = "Digi";
        public const int DigiId = 1004;

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

        public string GetUserIDforUsername(string username)
        {
            var user = db.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
            return user.Id;
        }

        public IdentityUser GetUserByUsername(string username)
        {
            return db.Users.Where(x => x.UserName.Equals(username)).FirstOrDefault();
        }

        public int GetAccountIdForBill(string bill)
        {
            switch (bill)
            {
                case AccountsService.Cez:
                    {
                        return AccountsService.CezId;
                    }
                case AccountsService.Digi:
                    {
                        return AccountsService.DigiId;
                    }
                case AccountsService.Enel:
                    {
                        return AccountsService.EnelId;
                    }
                case AccountsService.Orange:
                    {
                        return AccountsService.OrangeId;
                    }
                case AccountsService.Vodafone:
                    {
                        return AccountsService.VodafoneId;
                    }
            }

            return 99999;

        }

    }
}
