using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Data;
using MyBank.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MyBank.Models;
using Microsoft.EntityFrameworkCore;

namespace MyBank.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly ApplicationDbContext db;
        private readonly IAccountsService accountService;
        public TransactionsController(IAccountsService accountService, ApplicationDbContext db)
        {
            this.accountService = accountService;
            this.db = db;
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
        [HttpPost]
        public ActionResult Exchange(string fromCurrency, string toCurrency, float amount)
        {

            if(ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var userId = claim.Value;
                var accountId = accountService.GetAccountId(userId);
                var AmountLeft = accountService.GetBalanceForAccountIdAndCurrency(accountId, fromCurrency).Amount;
                AmountLeft = AmountLeft - amount;

                var transaction = new Transaction(accountId, accountId, DateTime.Now, fromCurrency, amount);
                var balance = new Balance();
                balance.AccountID = accountId;
                balance.Currency = fromCurrency;
                balance.Amount = AmountLeft;

                if (ModelState.IsValid)
                {
                    db.Entry(transaction).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }


            return Json(new { success = true});
        }

        // GET: TransactionsController/Create
        public ActionResult Create()
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
    }
}
