using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBank.Models;

namespace MyBankVer1.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        public int SenderID { get; set; }

        public int ReceiverID { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        public String Currency { get; set; }

        public float Amount { get; set; }

        [ForeignKey("SenderID")]
        public Account Sender { get; set; }

        [ForeignKey("ReceiverID")]
        public Account Receiver { get; set; }

        public Transaction(int TransactionID, int SenderID, int ReceiverID, DateTime Date, String Currency, float Amount)
        {
            this.TransactionID = TransactionID;
            this.SenderID = SenderID;
            this.ReceiverID = ReceiverID;
            this.Date = Date;
            this.Currency = Currency;
            this.Amount = Amount;
        }
    }
}
