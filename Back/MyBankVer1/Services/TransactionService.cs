using MyBank.Data;
using System;
using System.Linq;

namespace MyBank.Services
{
    public class TransactionService : ITransactionService
    {

        // constants

        public const string EURtoRON = "5";
        public const string EURtoUSD = "1.20";
        public const string RONtoUSD = "0.25";
        public const string RONtoEUR = "0.20";
        public const string USDtoRON = "4";
        public const string USDtoEUR = "0.80";

        private readonly ApplicationDbContext db;
        private readonly IAccountsService accountsService;

        public TransactionService(ApplicationDbContext db, IAccountsService accountsService)
        {
            this.db = db;
            this.accountsService = accountsService;
        }

        public void AddToAccount(int accountId, string currencyType, float amount)
        {
            var balance = db.Balances.Where(x => x.AccountID.Equals(accountId) && x.Currency.Equals(currencyType)).FirstOrDefault();
            balance.Amount += amount;
            db.SaveChanges();
        }

        public void DedudctFromAccount(int accountId, string currencyType, float amount)
        {
            var balance = db.Balances.Where(x => x.AccountID.Equals(accountId) && x.Currency.Equals(currencyType)).FirstOrDefault();
            balance.Amount -= amount;
            db.SaveChanges();
        }

        public float ConvertToCurrency(float amount, string currencyConstant)
        {
            amount /= float.Parse(currencyConstant);
            return amount;
        }

        // validation for transaction actions

        public bool validUsername(string username)
        {
            return accountsService.GetUserByUsername(username) != null ? true : false;                    
        }

        public bool validBalanceAmount(int accountId, float amount, string currency)
        {
            return accountsService.GetBalanceForAccountIdAndCurrency(accountId, currency).Amount >= amount ? true : false;
        }

    }
}