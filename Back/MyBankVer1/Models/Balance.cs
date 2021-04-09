using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBankVer1.Models;

namespace MyBank.Models
{
    public class Balance
    {
        [Key]
        public int BalanceID { get; set; }

        [ForeignKey("AccountID")]
        public Account Account { get; set; }

        public int AccountID { get; set; }

        public string Currency { get; set; }

        public Balance()
        {
        }
    }
}
