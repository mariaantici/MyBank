using Microsoft.AspNetCore.Mvc;
using MyBank.Models;
using MyBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBank.Controllers
{
    public class HistoryController : Controller
    {

        private readonly IHistoryService historyService;
        private readonly IAccountsService accountsService;

        public HistoryController(IHistoryService historyService, IAccountsService accountsService)
        {
            this.accountsService = accountsService;
            this.historyService = historyService;
        }
        public IActionResult TransactionHistory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetEntries()
        {
            List<Transaction> result;
            result = historyService.GetTransactions(this.User.FindFirstValue(ClaimTypes.NameIdentifier));         
            return Json(result);
        }

        [HttpGet]
        public IActionResult GetAccountIdForCurrentUser()
        {
            return Json(new { data = accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)) });
        }

        [HttpGet]
        public IActionResult GetUserNameForAccountId(int accountId)
        {
            return Json(new { data = accountsService.GetUserNameForAccountId(accountId) });
        }
    }
}
