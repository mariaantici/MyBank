using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBank.Models;

namespace MyBank.Models
{
    public class Account
    {
        [Key]
        public int AccountID { get; set; }

        public String UserID { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }

        public Account(String UserID)
        {
            this.UserID = UserID;
        }

        public ICollection<Transaction> SendTransactions { get; set; }
        public ICollection<Transaction> ReceiveTransactions { get; set; }
    }
}
