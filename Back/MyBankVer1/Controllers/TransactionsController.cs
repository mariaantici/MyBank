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
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionsController/Details/5
        public ActionResult Exchange()
        {
            return View();
        }

        // GET: TransactionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        public ActionResult Failure(string? errorType)
        {
            return View(new FailureModel(errorType));
        }


        [Authorize]
        public ActionResult Success()
        {
            return View();
        }

        // POST: TransactionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TransactionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Transfer()
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
            if (!transactionService.validBalanceAmount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), amount, type))
            {
                return RedirectToAction("Failure", new { errorType = "balance" });
            }

            transactionService.DedudctFromAccount(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)), type, amount);
            transactionService.AddToAccount(accountsService.GetAccountId(accountsService.GetUserIDforUsername(username)), type, amount);
            historyService.AddHistoryEntry(accountsService.GetAccountId(this.User.FindFirstValue(ClaimTypes.NameIdentifier)),
                                accountsService.GetAccountId(accountsService.GetUserIDforUsername(username)), DateTime.Now, type, amount);

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

    }
}
