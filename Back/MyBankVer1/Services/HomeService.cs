using MyBank.Data;
using MyBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public class HomeService : IHomeService
    {
        private readonly IApplicationDbContext db;
        private readonly AccountsService accountsService;
        public HomeService(IApplicationDbContext db)
        {
            this.db = db;
            accountsService = new AccountsService(db);

        }

        public float GetAccountAmount(int AccountId, string currency)
        {
            return db.Balances.FirstOrDefault(n => n.Currency == currency && n.AccountID == AccountId).Amount;
        }

        public UserBalance GetUserBalance(string userId, string currency)
        {
            var accountId = accountsService.GetAccountId(userId);
            var userName = accountsService.GetUserNameForAccountId(accountId);
            var amount = GetAccountAmount(accountId, currency);
            return new UserBalance(userName, currency, amount);


        }
    }
}
