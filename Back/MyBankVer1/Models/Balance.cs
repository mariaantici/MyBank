using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyBank.Models
{
    public class Balance
    {
        // constants

        public const string BALANCE_TYPE_EUR = "EUR";
        public const string BALANCE_TYPE_RON = "RON";
        public const string BALANCE_TYPE_USD = "USD";

        [Key]
        public int BalanceID { get; set; }

        [ForeignKey("AccountID")]
        public Account Account { get; set; }

        public int AccountID { get; set; }

        public string Currency { get; set; }

        public float Amount { get; set; }

        public Balance()
        {
        }
    }
}
