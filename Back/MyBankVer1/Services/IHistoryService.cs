using Microsoft.AspNetCore.Mvc;
using MyBank.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBank.Services
{
    public interface IHistoryService
    {
        void AddHistoryEntry(int senderId, int receiverId, DateTime date, string currencyType, float amount);
        public List<Transaction> GetTransactions(string userId);
    }
}