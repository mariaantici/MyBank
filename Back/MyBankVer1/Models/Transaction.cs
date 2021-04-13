using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyBank.Models;

namespace MyBank.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }

        public int SenderID { get; set; }

        public int ReceiverID { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime Date { get; set; }

        public String Currency { get; set; }

        public float Amount { get; set; }

        [ForeignKey("SenderID")]
        public Account Sender { get; set; }

        [ForeignKey("ReceiverID")]
        public Account Receiver { get; set; }

        public Transaction(int SenderID, int ReceiverID, DateTime Date, String Currency, float Amount)
        {            
            this.SenderID = SenderID;
            this.ReceiverID = ReceiverID;
            this.Date = Date;
            this.Currency = Currency;
            this.Amount = Amount;
        }
    }
}
