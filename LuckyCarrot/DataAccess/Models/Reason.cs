using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.Models
{
    public class Reason
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}
