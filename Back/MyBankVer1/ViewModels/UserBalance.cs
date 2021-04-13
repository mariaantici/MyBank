using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.ViewModels
{
    public class UserBalance
    {
        public string Name { get; set; }
        public string CurrencyType { get; set; }
        public double Amount { get; set; }

        public UserBalance(string name, string curencyType, float amount)
        {
            this.Name = name;
            this.CurrencyType = curencyType;
            this.Amount = amount;
        }
    }
}
