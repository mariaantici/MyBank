using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using MyBankVer1.Models;

namespace MyBank.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Account Account { get; set; }

        public ApplicationUser()
        {
        }
    }
}
