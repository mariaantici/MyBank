using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public interface IAccountsService
    {
        public int GetAccountId(string userId);
        public string GetUserNameForAccountId(int accountId);
    }
}
