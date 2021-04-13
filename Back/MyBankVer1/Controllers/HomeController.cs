using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyBankVer1.Data;
using MyBankVer1.Models;
using MyBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MyBankVer1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this.db = db;
        }

        [Authorize]
        public IActionResult Index(string id = "RON")
        {
            var currency = id;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            var userId = claim.Value;
            var userName = User.Identity.Name;
            var AccountId = db.Accounts.FirstOrDefault(n => n.UserID == userId);
            MyBank.Models.Balance currencyType = db.Balances.FirstOrDefault(n => n.Currency == currency && n.AccountID == AccountId.AccountID);

            var userBalance = new UserBalance(userName, currencyType.Currency, currencyType.Amount);

            return View("Index", userBalance);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
