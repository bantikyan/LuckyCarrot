using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
