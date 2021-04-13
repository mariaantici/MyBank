using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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
