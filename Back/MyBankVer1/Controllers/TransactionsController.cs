using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Services;
using MyBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyBank.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {

        private readonly ITransactionService transactionService;
        private readonly IAccountsService accountsService;
        private readonly IHistoryService historyService;


        public TransactionsController(ITransactionService transactionService, IAccountsService accountsService, IHistoryService historyService)
        {
            this.transactionService = transactionService;
            this.accountsService = accountsService;
            this.historyService = historyService;
        }

        // GET: TransactionsController
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionsController/Details/5
        [Authorize]
        public ActionResult Exchange()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Exchange(long amount, string fromCurrency, string toCurrency)
        {

            if (!transactionService.validBalanceAmount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), amount, fromCurrency))
            {
                return RedirectToAction("Failure", new { errorType = "balance" });
            }

            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;

            var convertedAmount = transactionService.GetExchangeRate(fromCurrency, toCurrency) * amount;

            transactionService.DedudctFromAccount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), fromCurrency, amount);
            transactionService.AddToAccount(accountsService.GetAccountId(userId), toCurrency, convertedAmount);
            historyService.AddHistoryEntry(accountsService.GetAccountId(userId),
                                accountsService.GetAccountId(userId), DateTime.Now, $"{fromCurrency}->{toCurrency}", amount);

            return RedirectToAction("Index", "History");
        }


        public ActionResult Transfer()
        {
            return View();
        }

        [Authorize]
        public ActionResult PayBills()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult PostTransfer(string username, long amount, string type)
        {

            if (!transactionService.validUsername(username))
            {
                return RedirectToAction("Failure", new { errorType = "username" });
            }

            var accountId = accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var senderId = accountId;
            var receiverId = accountsService.GetAccountId(accountsService.GetUserIDforUsername(username));

            if (!transactionService.validBalanceAmount(accountId, amount, type))
            {
                return RedirectToAction("Failure", new { errorType = "balance" });
            }

            transactionService.DedudctFromAccount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), type, amount);
            transactionService.AddToAccount(accountsService.GetAccountId(accountsService.GetUserIDforUsername(username)), type, amount);
            historyService.AddHistoryEntry(senderId, receiverId, DateTime.Now, type, amount);

            return RedirectToAction("Success");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostBillPay(string bill, long amount, string type)
        {
            if (!transactionService.validBalanceAmount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), amount, type))
            {
                return RedirectToAction("Failure", new { errorType = "balance" });
            }

            transactionService.DedudctFromAccount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), type, amount);
            transactionService.AddToAccount(accountsService.GetAccountIdForBill(bill), type, amount);
            historyService.AddHistoryEntry(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                                accountsService.GetAccountIdForBill(bill), DateTime.Now, type, amount);

            return RedirectToAction("Success");
        }

        [Authorize]
        public ActionResult Failure(string errorType)
        {
            return View(new FailureModel(errorType));
        }

        public ActionResult Success()
        {
            return View();
        }


    }
}
