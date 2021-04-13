using Microsoft.AspNetCore.Mvc;
using MyBank.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public interface IHistoryService
    {
        public List<Transaction> GetTransactions(string userId);
    }
}