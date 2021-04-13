using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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

        public float Amount { get; set; }

        public Balance()
        {
        }
    }
}
