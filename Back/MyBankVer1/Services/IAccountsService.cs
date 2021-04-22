using Microsoft.AspNetCore.Identity;
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
        Balance GetBalanceForAccountIdAndCurrency(int accountId, string currencyType);
        List<Balance> GetBalancesForAccountId(int accountId);
        IdentityUser GetUserByUsername(string username);
        string GetUserIDforUsername(string username);
        public string GetUserNameForAccountId(int accountId);

        int GetAccountIdForBill(string bill);
    }
}
