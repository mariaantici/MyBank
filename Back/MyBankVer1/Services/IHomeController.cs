using MyBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public interface IHomeController
    {
        public UserBalance GetUserBalance(string userId, string currency);
    }
}
