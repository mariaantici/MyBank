using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBank.ViewModels
{
    public class FailureModel
    {
        public FailureModel(string errorType)
        {
            this.errorType = errorType;
        }

        public string errorType { get; set; }
    }
}
