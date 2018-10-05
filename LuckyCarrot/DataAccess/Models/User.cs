using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public string ImageName { get; set; }
        public bool IsActive { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public int Points { get; set; }
        public int ReceivedPoints { get; set; }
    }
}
