using MyBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public interface IAccountsService
    {
        public int GetAccountId(string userId);
        List<Balance> GetBalancesForAccountId(int accountId);
        public string GetUserNameForAccountId(int accountId);

        public Balance GetBalanceForAccountIdAndCurrency(int accountId, string currencyType);
    }
}
